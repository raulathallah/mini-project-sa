using LibraryManagementSystem.Domain.Models.Entities;
using LibraryManagementSystem.Domain.Models.Requests;
using LibraryManagementSystem.Domain.Service;
using LibraryManagementSystem.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Pkcs;
using System.Data;

namespace LibraryManagementSystem.WebApi.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, ITokenService tokenService, UserManager<AppUser> userManager, IEmailService emailService, IUserService userService)
        {
            _authService = authService;
            _tokenService = tokenService;
            _userManager = userManager;
            _emailService = emailService;
            _userService = userService;
        }



        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AppUserRegister model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authService.Register(model);
            if (response.Status == false)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AppUserLogin model)
        {
            var response = await _authService.Login(model);
            if (response.Status == false)
            {
                return BadRequest(response);
            }

            SetRefreshTokenCookie("AuthToken", response.Token, response.ExpiredOn);
            SetRefreshTokenCookie("RefreshToken", response.RefreshToken, response.RefreshTokenExpiredOn);
            return Ok(response);
        }

        private void SetRefreshTokenCookie(string tokenType, string? token, DateTime? expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = expires
            };
            Response.Cookies.Append(tokenType, token, cookieOptions);
        }

        // POST: api/auth/logout
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
          /*  if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/

            /*            var response = await _authService.Logout(model.RefreshToken);
                        if (response.Status == false)
                        {
                            return BadRequest(response.Message);
                        }*/
            try
            {
                //hapus cookie
                Response.Cookies.Delete("AuthToken", new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                return Ok("Logout success!");

            }catch (Exception ex)
            {
                return StatusCode(500, "Error logout");
            }

            //return Ok(response);
        }
        // POST: api/Auth/role
        [HttpPost("role")]
        public async Task<IActionResult> CreateRole([FromBody]AddRoleRequest role)
        {
            var result = await _authService.CreateRole(role.RoleName);
            return Ok(result);
        }

        // POST: api/Auth/RefreshToken
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            // Validate the refresh token request.
            /*            if (request == null || string.IsNullOrWhiteSpace(request.RefreshToken))
                        {
                            return BadRequest("Refresh token is required.");  // Return bad request if no refresh token is provided.
                        }*/

            var refreshToken = Request.Cookies["RefreshToken"];

            try
            {
  
                // Retrieve the username associated with the provided refresh token.
                var username = await _tokenService.RetrieveUsernameByRefreshToken(refreshToken);
                if (string.IsNullOrEmpty(username))
                {
                    return BadRequest("Invalid refresh token.");  // Return unauthorized if no username is found (invalid or expired token).
                }
                // Retrieve the user by username.
                var user = await _userManager.FindByNameAsync(username);
                if (user == null)
                {
                    return BadRequest("Invalid user.");  // Return unauthorized if no user is found.
                }
                // Issue a new access token and refresh token for the user.
                var accessToken = await _tokenService.IssueAccessToken(user);
                var newRefreshToken = _tokenService.GenerateRefreshToken();

                // Save the new refresh token.
                var newRt = await _tokenService.SaveRefreshToken(user.UserName, newRefreshToken);
                var roles = await _userManager.GetRolesAsync(user);
                var userData = await _userService.GetUserByUsername(user.UserName);

                if (refreshToken == null)
                {
                    Response.Cookies.Delete("AuthToken", new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict
                    });
                    Response.Cookies.Delete("RefreshToken", new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict
                    });
                }
                else
                {
                    // set token cookie
                    SetRefreshTokenCookie("AuthToken", accessToken.Token, accessToken.ExpiredOn);
                    SetRefreshTokenCookie("RefreshToken", newRefreshToken, newRt.ExpiryDate);
                }


                // Return the new access and refresh tokens.
                return Ok(new { 
                    Token = accessToken.Token, 
                    TokenExpiredOn = accessToken.ExpiredOn, 
                    RefreshToken = newRefreshToken, 
                    RefreshTokenExpiredOn = newRt.ExpiryDate,
                    User = userData,
                    Roles = roles.ToArray(),
                });
            }
            catch (Exception ex)
            {
                // Handle any exceptions during the refresh process.
                return StatusCode(500, $"Internal server error: {ex.Message}");  // Return a 500 internal server error on exception.
            }
        }

        // POST: api/Auth/RevokeToken
        [HttpPost("RevokeToken")]
        public async Task<IActionResult> RevokeToken([FromBody] RefreshTokenRequest request)
        {
            // Validate the revocation request.
            if (request == null || string.IsNullOrWhiteSpace(request.RefreshToken))
            {
                return BadRequest("Refresh token is required.");  // Return bad request if no refresh token is provided.
            }
            try
            {
                // Attempt to revoke the refresh token.
                var result = await _tokenService.RevokeRefreshToken(request.RefreshToken);
                if (!result)
                {
                    return NotFound("Refresh token not found.");  // Return not found if the token does not exist.
                }
                return Ok("Token revoked.");  // Return success message if the token is successfully revoked.
            }
            catch (Exception ex)
            {
                // Handle any exceptions during the revocation process.
                return StatusCode(500, $"Internal server error: {ex.Message}");  // Return a 500 internal server error on exception.
            }
        }

        // POST: api/Auth/email
        [HttpPost("email")]
        public IActionResult SendEmail([FromBody] MailData request)
        {
            if(request == null)
            {
                return BadRequest();
            }

            var emailBody = System.IO.File.ReadAllText(@"./EmailTemplate.html");
            
            emailBody = string.Format(emailBody,
                    "Library Management System",
                    DateTime.UtcNow
                );
            request.EmailBody = emailBody;
            var response = _emailService.SendMail(request);
            return Ok(response);
        }


    }
}

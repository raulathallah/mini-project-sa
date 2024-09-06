using CompanyWeb.Domain.Models.Auth;
using CompanyWeb.Domain.Models.Requests;
using CompanyWeb.Domain.Models.Requests.Add;
using CompanyWeb.Domain.Models.Requests.Update;
using CompanyWeb.Domain.Services;
using CompanyWeb.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CompanyWeb.WebApi.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        public AuthController(IAuthService authService,
            UserManager<AppUser> userManager,
            IUserService userService)
        {
            _authService = authService;
            _userManager = userManager;
            _userService = userService;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AppUserLogin model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authService.Login(model);
            if (response.Status == false)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }

        // POST: api/Auth/RefreshToken
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            // Validate the refresh token request.
            if (request == null || string.IsNullOrWhiteSpace(request.RefreshToken))
            {
                return BadRequest("Refresh token is required.");  // Return bad request if no refresh token is provided.
            }
            try
            {
                // Retrieve the username associated with the provided refresh token.
                var username = await _authService.RetrieveUsernameByRefreshToken(request.RefreshToken);
                if (string.IsNullOrEmpty(username))
                {
                    return Unauthorized("Invalid refresh token.");  // Return unauthorized if no username is found (invalid or expired token).
                }
                // Retrieve the user by username.
                var user = await _userManager.FindByNameAsync(username);
                if (user == null)
                {
                    return Unauthorized("Invalid user.");  // Return unauthorized if no user is found.
                }
                // Issue a new access token and refresh token for the user.
                var tokens = await _authService.GetTokens(user);

                // Save the new refresh token
                if (user.RefreshTokenExpiredOn > DateTime.UtcNow)
                {
                    user.RefreshToken = tokens.NewRefreshToken;
                }
                // Return the new access and refresh tokens.
                return Ok(new { Token = tokens.AccessToken.Token, TokenExpiredOn = tokens.AccessToken.ExpiredOn, RefreshToken = user.RefreshToken, RefreshTokenExpiredOn = user.RefreshTokenExpiredOn });
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
                var result = await _authService.RevokeRefreshToken(request.RefreshToken);
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

        // POST: api/auth/register
        [Authorize(Roles = "Administrator")]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AppUserRegister model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _userService.Register(model);
            if (response.Status == false)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }

        // POST: api/auth/appUserId
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{appUserId}")]
        public async Task<IActionResult> Delete([FromRoute]string appUserId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _userService.DeleteUser(appUserId);
            if (response.Status == false)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }

        // PUT: api/auth/appUserId
        [Authorize(Roles = "Administrator")]
        [HttpPut("{appUserId}")]
        public async Task<IActionResult> Update([FromRoute]string appUserId, [FromBody] UpdateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _userService.UpdateUser(appUserId, request);
            if (response.Status == false)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }
        // GET: api/auth
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _userService.GetAllUser();
            return Ok(response);
        }
    }
}

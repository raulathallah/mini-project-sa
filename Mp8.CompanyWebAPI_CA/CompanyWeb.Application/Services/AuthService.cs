using CompanyWeb.Domain.Models.Auth;
using CompanyWeb.Domain.Models.Helpers;
using CompanyWeb.Domain.Models.Requests;
using CompanyWeb.Domain.Models.Requests.Add;
using CompanyWeb.Domain.Models.Responses;
using CompanyWeb.Domain.Repositories;
using CompanyWeb.Domain.Services;
using LibraryManagementSystem.Domain.Models.Responses;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeService _employeeService;

        public AuthService(UserManager<AppUser> userManager,
            IConfiguration configuration,
            IEmployeeRepository employeeRepository,
            IEmployeeService employeeService,
            IUserService userService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
            _userService = userService;
        }

        public async Task<TokensResponse> GetTokens(AppUser user)
        {
            var accessToken = await IssueAccessToken(user);
            var refreshToken = GenerateRefreshToken();

            return new TokensResponse()
            {
                AccessToken = accessToken,
                NewRefreshToken = refreshToken
            };
        }

        public async Task<AppUserResponse> Login(AppUserLogin model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            var emp = await _employeeService.GetEmployeeByAppUserId(user.Id);
            var roles = await _userManager.GetRolesAsync(user);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {

                var tokens = await this.GetTokens(user);
                user.RefreshToken = tokens.NewRefreshToken;
                if (user.RefreshToken == null)
                {
                    user.RefreshTokenExpiredOn = DateTime.UtcNow.AddDays(7);  
                }
                await _userManager.UpdateAsync(user);
                return new AppUserResponse()
                {
                    Token = tokens?.AccessToken?.Token,
                    RefreshToken = tokens?.NewRefreshToken,
                    RefreshTokenExpiredOn = user.RefreshTokenExpiredOn,
                    ExpiredOn = tokens?.AccessToken?.ExpiredOn,
                    Status = true,
                    User = user,
                    Employee = emp,
                    Roles = roles.ToArray(),
                    Message = "Login success!"
                };
            }

            return new AppUserResponse()
            {
                Token = null,
                ExpiredOn = null,
                Status = false,
                Message = "Credentials not valid!"
            };
        } 

        public async Task<AppUserResponse> Logout(string refreshToken)
        {
            bool IsRevoked = await this.RevokeRefreshToken(refreshToken);

            if (IsRevoked == true)
            {
                return new AppUserResponse()
                {
                    Message = "Token revoked!"
                };
            }
            return new AppUserResponse()
            {
                Message = "Refresh token not found"
            };
        }



        public async Task<string> RetrieveUsernameByRefreshToken(string refreshToken)
        {
            var username = await _userManager.FindUsernameByRefreshTokenAsync(refreshToken);
            if (username == null)
            {
                return null;
            }
            return username;
        }

        public async Task<bool> RevokeRefreshToken(string refreshToken)
        {
            var user = await _userManager.FindByRefreshTokenAsync(refreshToken);
            if (user == null)
            {
                return false;
            }
            user.RefreshToken = null;
            user.RefreshTokenExpiredOn = null;
            await _userManager.UpdateAsync(user);
            return true;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];  // Prepare a buffer to hold the random bytes.
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);  // Fill the buffer with cryptographically strong random bytes.
                return Convert.ToBase64String(randomNumber);  // Convert the bytes to a Base64 string and return.
            }
        }

        private async Task<IssueAccessTokenResponse> IssueAccessToken(AppUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var expiredDate = DateTime.Now.AddHours(3);
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: expiredDate,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );


            return new IssueAccessTokenResponse()
            {
                ExpiredOn = expiredDate,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}

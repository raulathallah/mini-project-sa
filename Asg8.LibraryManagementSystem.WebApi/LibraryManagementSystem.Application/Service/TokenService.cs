using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.Domain.Models.Entities;
using LibraryManagementSystem.Domain.Models.Responses;
using LibraryManagementSystem.Domain.Repositories;
using LibraryManagementSystem.Domain.Service;
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

namespace LibraryManagementSystem.Application.Service
{
    public class TokenService : ITokenService
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        public TokenService(ITokenRepository tokenRepository, IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _tokenRepository = tokenRepository;
            _configuration = configuration;
            _userManager = userManager;
        }

        public string CreateToken(AppUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<string> RetrieveUsernameByRefreshToken(string refreshToken)
        {
            var tokenRecord = await _tokenRepository.Get(refreshToken);
            if(tokenRecord == null)
            {
                return null;
            }
            return tokenRecord.Username;
        }

        public async Task<bool> RevokeRefreshToken(string refreshToken)
        {
            var tokenRecord = await _tokenRepository.Get(refreshToken);
            if( tokenRecord == null)
            {
                return false;
            }
            await _tokenRepository.Delete(refreshToken);
            return true;
        }

        public async Task<RefreshToken> SaveRefreshToken(string username, string token)
        {
            var user = await _userManager.FindByNameAsync(username);
            if(user == null)
            {
                return null;
            }
            var rt = new RefreshToken()
            {
                Username = username,
                AppUserId = user.Id,
                Token = token,
                ExpiryDate = DateTime.UtcNow.AddDays(7)
            };

            var findRt = await _tokenRepository.GetRefreshTokenByAppUserId(user.Id);
            if(findRt != null)
            {
                return await this.UpdateRefreshToken(user.Id, token);
            }

            return await _tokenRepository.Create(rt);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];  // Prepare a buffer to hold the random bytes.
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);  // Fill the buffer with cryptographically strong random bytes.
                return Convert.ToBase64String(randomNumber);  // Convert the bytes to a Base64 string and return.
            }
        }

        public async Task<IssueAccessTokenResponse> IssueAccessToken(AppUser user)
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
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]));
            var expiredDate = DateTime.UtcNow.AddHours(8);
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

        public async Task<RefreshToken> UpdateRefreshToken(string appUserId, string token)
        {
            var findRt = await _tokenRepository.GetRefreshTokenByAppUserId(appUserId);
            if (findRt == null)
            {
                return null;
            }
            findRt.Token = token;
            findRt.ExpiryDate = DateTime.UtcNow.AddDays(7);
            await _tokenRepository.Update(findRt); 
            return findRt;
        }
    }
}

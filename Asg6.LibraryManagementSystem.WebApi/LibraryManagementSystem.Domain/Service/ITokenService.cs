using LibraryManagementSystem.Domain.Models.Entities;
using LibraryManagementSystem.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Service
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);

        Task<RefreshToken> SaveRefreshToken(string username, string token);
        Task<RefreshToken> UpdateRefreshToken(string username, string token);
        Task<string> RetrieveUsernameByRefreshToken(string refreshToken);
        Task<bool> RevokeRefreshToken(string refreshToken);
        string GenerateRefreshToken();
        Task<IssueAccessTokenResponse> IssueAccessToken(AppUser user);
    }
}

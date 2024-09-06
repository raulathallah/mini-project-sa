using CompanyWeb.Domain.Models.Auth;
using CompanyWeb.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Services
{
    public interface IAuthService
    {
        Task<AppUserResponse> Login(AppUserLogin model);
        Task<AppUserResponse> Logout(string refreshToken);

        // Token //
        Task<string> RetrieveUsernameByRefreshToken(string refreshToken);
        Task<TokensResponse> GetTokens(AppUser user);
        Task<bool> RevokeRefreshToken(string refreshToken);

    }
}

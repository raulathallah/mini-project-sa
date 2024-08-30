using LibraryManagementSystem.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Repositories
{
    public interface ITokenRepository
    {
        Task<RefreshToken> Create(RefreshToken token);
        Task<RefreshToken> Update(RefreshToken token);
        Task<RefreshToken> Get(string rt);
        Task<RefreshToken> Delete(string rt);
        Task<RefreshToken> GetRefreshTokenByAppUserId(string appUserId);
    }
}

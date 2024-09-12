using LibraryManagementSystem.Domain.Models.Entities;
using LibraryManagementSystem.Domain.Repositories;
using LibraryManagementSystem.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Infrastructure.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly LMSDbContext Context;
        private readonly UserManager<AppUser> _userManager;
        public TokenRepository(LMSDbContext context, UserManager<AppUser> userManager)
        {
            Context = context;
            _userManager = userManager;
        }
        public async Task<RefreshToken> Create(RefreshToken token)
        {

            Context.RefreshTokens.Add(token);
            await Context.SaveChangesAsync();
            return token;
        }
        public async Task<RefreshToken> Update(RefreshToken token)
        {
            Context.RefreshTokens.Update(token);
            await Context.SaveChangesAsync();
            return token;
        }
        public async Task<RefreshToken> Get(string rt)
        {
            var token = Context.RefreshTokens.Where(w => w.Token == rt && w.ExpiryDate > DateTime.UtcNow).FirstOrDefault();
            return token;
        }
        public async Task<RefreshToken> GetRefreshTokenByAppUserId(string appUserId)
        {
            var token = Context.RefreshTokens.Where(w => w.AppUserId == appUserId && w.ExpiryDate > DateTime.UtcNow).FirstOrDefault();
            return token;
        }

        public async Task<RefreshToken> Delete(string rt)
        {
            var token = Context.RefreshTokens.Where(w => w.Token == rt).FirstOrDefault();
            Context.RefreshTokens.Remove(token);
            await Context.SaveChangesAsync();
            return token;
        }
    }
}

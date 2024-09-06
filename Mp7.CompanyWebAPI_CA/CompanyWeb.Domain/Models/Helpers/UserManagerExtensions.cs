using CompanyWeb.Domain.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Helpers
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> FindByRefreshTokenAsync(this UserManager<AppUser> um, string refreshToken)
        {
            return await um.Users.Where(x => x.RefreshToken == refreshToken).FirstOrDefaultAsync();
        }

        public static async Task<string> FindUsernameByRefreshTokenAsync(this UserManager<AppUser> um, string refreshToken)
        {
            return await um.Users.Where(x => x.RefreshToken == refreshToken).Select(s=>s.UserName).FirstOrDefaultAsync();
        }
    }
}

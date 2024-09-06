using LibraryManagementSystem.Domain.Models.Entities;
using LibraryManagementSystem.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Service
{
    public interface IAuthService
    {
        Task<AppUserResponse> Register(AppUserRegister model);
        Task<AppUserResponse> Login(AppUserLogin model);
        Task<AppUserResponse> Logout(string refreshToken);


        // Role //
        Task<AppUserResponse> CreateRole(string roleName);
    }
}

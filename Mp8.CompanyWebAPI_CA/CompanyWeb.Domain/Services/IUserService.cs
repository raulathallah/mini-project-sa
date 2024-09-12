using CompanyWeb.Domain.Models.Auth;
using CompanyWeb.Domain.Models.Requests.Update;
using CompanyWeb.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Services
{
    public interface IUserService
    {
        Task<AppUserResponse> Register(AppUserRegister model);
        Task<UserResponse> UpdateUser(string appUserId, UpdateUserRequest request);
        Task<UserResponse> DeleteUser(string appUserId);
        //Task<UserResponse> GetUser(AppUser user);
        Task<List<object>> GetAllUser();

    }
}

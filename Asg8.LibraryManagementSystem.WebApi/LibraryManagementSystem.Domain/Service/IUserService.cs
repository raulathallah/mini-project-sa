using LibraryManagementSystem.Application.Helpers;
using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.Domain.Models.Requests.Books;
using LibraryManagementSystem.Domain.Models.Requests.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Service
{
    public interface IUserService
    {
        Task<IEnumerable<object>> GetAllUser();
        Task<User> GetUserByUsername(string username);
        Task<User> GetUserById(int userId);
        Task<User> AddNewUser(AddUserRequest request);
        Task<User> UpdateUser(int userId, UpdateUserRequest request);
        Task<User> DeleteUser(int userId);

        // Generate Report //
        Task<byte[]> GenerateUserReportPDF(string appUserId);
    }
}

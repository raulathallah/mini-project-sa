using LMS.Core.Models;
using LMS.Core.Models.Requests;
using LMS.Domain.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.IRepositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUser();
        Task<User> GetUserById(int userId);
        Task<User> AddNewUser(AddUserRequest request);
        Task<User> UpdateUser(int userId, UpdateUserRequest request);
        Task<User> DeleteUser(int userId);
    }
}

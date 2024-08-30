using LibraryManagementSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IQueryable<User>> GetAll();
        Task<User> Get(int userId);
        Task<User> GetByAppUserId(string appUserId);
        Task<User> GetByLibraryCardNumber(string lcn);
        Task<User> Add(User user);
        Task<User> Update(User user);
        Task<User> Delete(User user);
    }
}

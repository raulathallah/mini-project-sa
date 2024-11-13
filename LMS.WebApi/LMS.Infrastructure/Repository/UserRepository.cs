using LMS.Core.IRepositories;
using LMS.Core.Models;
using LMS.Core.Models.Requests;
using LMS.Domain.Models.Requests;
using LMS.Infrastructure.Factory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly LMSDbContext _context;

        public UserRepository(LMSDbContext context)
        {
            _context = context;
        }

        public async Task<User> AddNewUser(AddUserRequest request)
        {
            if (_context.Users.Where(w => w.Phonenumber == request.Phonenumber).FirstOrDefault() != null)
            {
                return null;
            }

            User newUser = UserFactory.CreateUser(request);
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        public async Task<User> DeleteUser(int id)
        {
            var deleteUser = await _context.Users.Where(w => w.Userid == id).FirstOrDefaultAsync();
            if (deleteUser == null)
            {
                return null;
            }
            _context.Users.Remove(deleteUser);
            await _context.SaveChangesAsync();
            return deleteUser;
        }

        public async Task<List<User>> GetAllUser()
        {
            return await _context.Users.ToListAsync();

        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user;
        }

        public async Task<User> UpdateUser(int id, UpdateUserRequest request)
        {
            var updateUser = _context.Users.Where(w => w.Userid == id).FirstOrDefault();
            if (updateUser == null)
            {
                return null;
            }

            updateUser.Phonenumber = request.Phonenumber;
            updateUser.Username = request.Username;

            _context.Users.Update(updateUser);
            await _context.SaveChangesAsync();
            return updateUser;
        }
    }
}

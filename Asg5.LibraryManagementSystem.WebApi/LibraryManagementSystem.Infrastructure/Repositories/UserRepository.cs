using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.Domain.Repositories;
using LibraryManagementSystem.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LMSDbContext Context;
        public UserRepository(LMSDbContext context)
        {
            Context = context;
        }

        public async Task<User> Add(User user)
        {
            Context.Users.Add(user);
            await Context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Delete(User user)
        {
            Context.Users.Remove(user);
            await Context.SaveChangesAsync();
            return user;
        }

        public async Task<IQueryable<User>> GetAll()
        {
            return Context.Users;

        }

        public async Task<User> Get(int userId)
        {
            return await Context.Users.FindAsync(userId);
        }

        public async Task<User> Update(User user)
        {
            Context.Users.Update(user);
            await Context.SaveChangesAsync();
            return user;
        }
    }
}

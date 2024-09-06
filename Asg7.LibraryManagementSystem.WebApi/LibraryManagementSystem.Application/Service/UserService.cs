using LibraryManagementSystem.Application.Mappers;
using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.Domain.Models.Entities;
using LibraryManagementSystem.Domain.Models.Requests.Books;
using LibraryManagementSystem.Domain.Models.Requests.Users;
using LibraryManagementSystem.Domain.Repositories;
using LibraryManagementSystem.Domain.Service;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;

        public UserService(IUserRepository userRepository, UserManager<AppUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<User> AddNewUser(AddUserRequest request)
        {
            var newUser = new User()
            {
                FName = request.fName,
                LName = request.lName,
                UserPosition = request.UserPosition,
                UserPrivilage = request.UserPrivilage,
                LibraryCardExpiredDate = DateOnly.MaxValue,
                LibraryCardNumber = "",
            };
            var user = await _userRepository.Add(newUser);
            if(user.UserPosition != "Library User")
            {
                user.LibraryCardNumber = await GenerateLibraryCardNumber(user);
                user.LibraryCardExpiredDate = DateOnly.FromDateTime(DateTime.Now.AddYears(5));
                await _userRepository.Update(user);
            }
            return user;
        }

        public async Task<User> DeleteUser(int userId)
        {
            var user = await _userRepository.Get(userId);
            if (user == null)
            {
                return null;
            }
            var deleteUser = await _userRepository.Delete(user);
            return deleteUser;
        }

        public async Task<IEnumerable<object>> GetAllUser()
        {
            var users = await _userRepository.GetAll();
            return users.Select(s => s.ToUserResponse());
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _userRepository.Get(userId);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var allUser = await _userRepository.GetAll();
            var user = allUser.Where(w=>w.FName == username).FirstOrDefault();
            return user;
        }

        public async Task<User> UpdateUser(int userId, UpdateUserRequest request)
        {
            var user = await _userRepository.Get(userId);
            if(user == null)
            {
                return null;
            }
            user.FName = request.fName;
            user.LName = request.lName;
            user.UserPosition = request.UserPosition;
            user.UserPrivilage = request.UserPrivilage;
            user.UserNotes = request.UserNotes;

            var updateUser = await _userRepository.Update(user);
            return updateUser;
        }


        private async Task<string> GenerateLibraryCardNumber(User user)
        {
            var random = new Random().Next(1000,10000);
            var number = $"{user.UserId}-{random}";

            var users = await _userRepository.GetAll();

            if (users.Any(a=>a.LibraryCardNumber == number))
            {
                GenerateLibraryCardNumber(user);
            }

            return number;
        }
    }
}

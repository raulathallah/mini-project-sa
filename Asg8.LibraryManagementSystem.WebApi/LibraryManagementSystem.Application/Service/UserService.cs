using LibraryManagementSystem.Application.Mappers;
using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.Domain.Helpers;
using LibraryManagementSystem.Domain.Models.Entities;
using LibraryManagementSystem.Domain.Models.Requests.Books;
using LibraryManagementSystem.Domain.Models.Requests.Users;
using LibraryManagementSystem.Domain.Repositories;
using LibraryManagementSystem.Domain.Service;
using LMS.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
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
        private readonly LibraryOptions _libraryOptions;
        private readonly IPdfService _pdfService;
        private readonly IBookUserTransactionRepository _bookUserTransactionRepository;
        private readonly IBookRepository _bookRepository;

        public UserService(IUserRepository userRepository, 
            UserManager<AppUser> userManager, 
            IOptions<LibraryOptions> libraryOptions, 
            IPdfService pdfService,
            IBookUserTransactionRepository bookUserTransactionRepository,
            IBookRepository bookRepository)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _libraryOptions = libraryOptions.Value;
            _pdfService = pdfService;
            _bookUserTransactionRepository = bookUserTransactionRepository;
            _bookRepository = bookRepository;
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

        //SRS-036
        public async Task<byte[]> GenerateUserReportPDF(string appUserId)
        {
            string htmlcontent = String.Empty;
            var bookTransaction = await _bookUserTransactionRepository.GetAll();
            var user = await _userRepository.GetByAppUserId(appUserId);
            var appUser = await _userManager.FindByIdAsync(appUserId);
            var allBook = await _bookRepository.GetAll();
            var bookCategories = allBook.Select(s => s.Category).Distinct().ToList();



            htmlcontent += "<h3>User's Information</h3>";
            htmlcontent += "<table>";
            htmlcontent +="<tr><td>User ID</td><td>"+user.UserId+"</td></tr>";
            htmlcontent +="<tr><td>Email</td><td>"+ appUser.Email + "</td></tr>";
            htmlcontent +="<tr><td>Name</td><td>"+ user.FName + " " + user.LName + "</td></tr>";
            htmlcontent +="<tr><td>Position</td><td>"+ user.UserPosition + "</td></tr>";
            htmlcontent +="<tr><td>Library Card Number</td><td>"+ user.LibraryCardNumber + "</td></tr>";
            htmlcontent +="<tr><td>Library Card Expired Date</td><td>"+ user.LibraryCardNumber + "</td></tr>";
            htmlcontent += "</table>";

            htmlcontent += "<h3>User's Transaction Information</h3>";
            htmlcontent += "<table>";
            htmlcontent +=
                "<tr>" +
                "<td>Title</td>" +
                "<td>Isbn</td>" +
                "<td>Borrowed At</td>" +
                "<td>Due Date</td>" +
                "<td>Penalty</td>" +
                "</tr>";
            foreach(var b in bookTransaction.Where(w=>w.AppUserId == appUserId))
            {

                var overdue = DateOnly.FromDateTime(DateTime.Now).Day - b.DueDate.Day;
                if(overdue < 0)
                {
                    overdue = 0;
                }

                htmlcontent += "<tr>";
                htmlcontent += "<td>" + b.Title + "</td>";
                htmlcontent += "<td>" + b.Isbn + "</td>";
                htmlcontent += "<td>" + b.BorrowedAt + "</td>";
                htmlcontent += "<td>" + b.DueDate + "</td>";
                htmlcontent += "<td>" + overdue * _libraryOptions.PenaltyPerDay + "</td>";
                htmlcontent += "</tr>";
            }
            htmlcontent += "</table>";

            return _pdfService.OnGeneratePDF(htmlcontent);

        }
    }
}

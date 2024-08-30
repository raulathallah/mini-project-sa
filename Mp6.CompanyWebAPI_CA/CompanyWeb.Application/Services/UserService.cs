using CompanyWeb.Domain.Models.Auth;
using CompanyWeb.Domain.Models.Requests;
using CompanyWeb.Domain.Models.Requests.Update;
using CompanyWeb.Domain.Models.Responses;
using CompanyWeb.Domain.Repositories;
using CompanyWeb.Domain.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeService _employeeService;

        public UserService(UserManager<AppUser> userManager,
            IEmployeeRepository employeeRepository,
            IEmployeeService employeeService)
        {
            _userManager = userManager;
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
        }

        public async Task<AppUserResponse> Register(AppUserRegister model)
        {
            var userExist = await _userManager.FindByNameAsync(model.UserName);
            if (userExist != null)
            {
                return new AppUserResponse()
                {
                    ExpiredOn = null,
                    Token = null,
                    Status = false,
                    Message = "User already exists!"
                };
            }
            AppUser user = new AppUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return new AppUserResponse()
                {
                    ExpiredOn = null,
                    Token = null,
                    Status = false,
                    Message = "User creation failed! Please check user details and try again."
                };
            }


            // update to table employee
            var justCreatedUser = await _userManager.FindByNameAsync(model.UserName);
            if (justCreatedUser == null)
            {
                return null;
            }

            var allEmp = await _employeeRepository.GetAllEmployees();
            var empData = allEmp.Where(w => w.EmailAddress.Equals(justCreatedUser.Email)).FirstOrDefault();

            if (empData == null)
            {
                await _userManager.DeleteAsync(user);

                return new AppUserResponse()
                {
                    ExpiredOn = null,
                    Token = null,
                    Status = true,
                    Message = "No employee with email inputed exist!"
                };
            }
            empData.AppUserId = justCreatedUser.Id;
            empData.IsActive = true;
            empData.DeactivateReason = null;
            await _employeeRepository.Update(empData);
            await _userManager.AddToRoleAsync(user, model.Role);
            return new AppUserResponse()
            {
                ExpiredOn = null,
                Token = null,
                Status = true,
                Message = "User created successfully!"
            };
        }

        public async Task<UserResponse> DeleteUser(string appUserId)
        {
            var user = await _userManager.FindByIdAsync(appUserId);
            if(user == null)
            {
                return new UserResponse()
                {
                    Message = "Delete failed, user not found",
                };
            }

            var deleteUser = await _userManager.DeleteAsync(user);
            if(!deleteUser.Succeeded)
            {
                return new UserResponse()
                {
                    Message = "Delete failed, user not found",
                };
            }

            var allEmp = await _employeeRepository.GetAllEmployees();
            var empData = allEmp.Where(w => w.AppUserId == user.Id).FirstOrDefault();
            if (empData == null)
            {
                return new UserResponse()
                {
                    AppUserId = null,
                    Email = null,
                    Message = "Delete failed, employee data not found",
                    Status = false,
                };
            }
            var dreq = new DeactivateEmployeeRequest()
            {
                DeactivateReason = "USER DELETED"
            };
            await _employeeService.DeactivateEmployee(empData.Empno, dreq);

            return new UserResponse()
            {
                AppUserId = user.Id,
                Email = user.Email,
                Message = "Delete succcess",
                Status = true,
            };

        }

        public async Task<List<object>> GetAllUser()
        {
            var users = _userManager.Users;

            return users.Select(u => new
            {
                AppUserId = u.Id,
                Email = u.Email,
                UserName = u.UserName
            })
            .ToList<object>();
        }

        public Task<UserResponse> GetUser(AppUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<UserResponse> UpdateUser(string appUserId, UpdateUserRequest request)
        {
            var user = await _userManager.FindByIdAsync(appUserId);
            if (user == null)
            {
                return new UserResponse()
                {
                    Message = "Delete failed, user not found",
                };
            }
            var allEmp = await _employeeRepository.GetAllEmployees();
            var emp = allEmp.Where(w => w.AppUserId == user.Id).FirstOrDefault();
             
            emp.EmailAddress = request.Email;
            user.Email = request.Email;
            user.UserName = request.UserName;

            await _userManager.UpdateAsync(user);
            await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
   
            return new UserResponse()
            {
                AppUserId = user.Id,
                Email = user.Email,
                Message = "Update succcess",
                Status = true,
            };
        }
    }
}

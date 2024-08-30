using CompanyWeb.Domain.Models.Auth;
using CompanyWeb.Domain.Models.Responses;
using CompanyWeb.Domain.Repositories;
using CompanyWeb.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IRoleRepository _roleRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public RoleService(RoleManager<IdentityRole> roleManager,
            UserManager<AppUser> userManager,
            IRoleRepository roleRepository,
            IEmployeeRepository employeeRepository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _roleRepository = roleRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<object> AssignUserRole(string appUserId, string roleId)
        {
            var allEmp = await _employeeRepository.GetAllEmployees();
            var emp = allEmp.Where(w => w.AppUserId == appUserId).FirstOrDefault();

            var user = await _userManager.FindByIdAsync(appUserId);
            var role = await _roleManager.FindByIdAsync(roleId);
            var roleName = await _roleManager.GetRoleNameAsync(role);

            if(roleName == "Administrator")
            {
                return new
                {
                    AppUserId = "",
                    Role = "",
                    Message = "Cannot assign to Administrator"
                };
            }

            if(roleName == "Employee Supervisor" && !emp.Position.ToLower().Contains("supervisor".ToLower()))
            {
                return new
                {
                    AppUserId = "",
                    Role = "",
                    Message = "Cannot assign to Employee Supervisor"
                };
            }



            await _userManager.AddToRoleAsync(user, roleName);

            return new
            {
                AppUserId = appUserId,
                Role = roleName,
                Message = "User assigned"
            };
        }
        public async Task<object> RevokeUserRole(string appUserId, string roleId)
        {
            var user = await _userManager.FindByIdAsync(appUserId);
            var role = await _roleManager.FindByIdAsync(roleId);
            var roleName = await _roleManager.GetRoleNameAsync(role);

            await _userManager.RemoveFromRoleAsync(user, roleName);

            if (roleName == "Administrator")
            {
                return new
                {
                    AppUserId = "",
                    Role = "",
                    Message = "Cannot revoke role Administrator"
                };
            }
            return new
            {
                AppUserId = appUserId,
                Role = roleName,
                Message = "Role revoked"
            };
        }
        public async Task<RoleResponse> DeleteRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if(role == null)
            {
                return new RoleResponse()
                {
                    Message = "No role found",
                    RoleName = null,
                    Status = true
                };
            }


            await _roleManager.DeleteAsync(role);
            return new RoleResponse()
            {
                Message = "Delete role success",
                RoleName = role.Name,
                Status = true
            };
        }
        public async Task<RoleResponse> CreateRole(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName) && !string.IsNullOrWhiteSpace(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
                return new RoleResponse()
                {
                    RoleName = roleName,
                    Status = true,
                    Message = "Role created successfully!"
                };
            }
            return new RoleResponse()
            {
                Message = "Role name invalid!"
            };


        }

        public async Task<IEnumerable<IdentityRole>> GetAllRole()
        {
            var roles = await _roleRepository.GetAllRole();
            return roles;
        }

        public async Task<RoleResponse> UpdateRole(string roleId, string roleName)
        {
            var findRole = await _roleManager.FindByIdAsync(roleId);

            if(findRole == null)
            {
                return new RoleResponse()
                {
                    Message = "Role not found"
                };
            }
            findRole.Name = roleName;
            await _roleManager.UpdateAsync(findRole);
            return new RoleResponse()
            {
                RoleName = roleName,
                Status = true,
                Message = "Role update successfully!"
            };
        }
    }
}

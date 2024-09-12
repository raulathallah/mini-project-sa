using CompanyWeb.Domain.Models.Responses;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Services
{
    public interface IRoleService
    {
        // Role //
        Task<RoleResponse> CreateRole(string roleName);
        Task<RoleResponse> UpdateRole(string roleId, string roleName);
        Task<RoleResponse> DeleteRole(string roleId);
        Task<IEnumerable<IdentityRole>> GetAllRole();
        Task<object> AssignUserRole(string appUserId, string roleId);
        Task<object> RevokeUserRole(string appUserId, string roleId);
    }
}

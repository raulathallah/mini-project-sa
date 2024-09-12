using CompanyWeb.Domain.Models.Requests;
using CompanyWeb.Domain.Models.Requests.Add;
using CompanyWeb.Domain.Models.Requests.Update;
using CompanyWeb.Domain.Services;
using CompanyWeb.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyWeb.WebApi.Controllers
{
    public class RolesController : BaseController
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }


        // POST: api/Roles
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] AddRoleRequest role)
        {
            var result = await _roleService.CreateRole(role.RoleName);
            return Ok(result);
        }

        // PUT: api/Roles/1
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole([FromRoute] string id, [FromBody] UpdateRoleRequest role)
        {
            var result = await _roleService.UpdateRole(id, role.RoleName);
            return Ok(result);
        }

        // GET: api/Roles
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetAllRole()
        {
            var result = await _roleService.GetAllRole();
            return Ok(result);
        }

        // POST: api/Roles/assign
        [Authorize(Roles = "Administrator")]
        [HttpPost("assign")]
        public async Task<IActionResult> AssignUserROle([FromBody] AssignUserRequest request)
        {
            var result = await _roleService.AssignUserRole(request.AppUserId, request.RoleId);
            return Ok(result);
        }

        // POST: api/Roles/revoke
        [Authorize(Roles = "Administrator")]
        [HttpPost("revoke")]
        public async Task<IActionResult> RevokeUserRole([FromBody] AssignUserRequest request)
        {
            var result = await _roleService.RevokeUserRole(request.AppUserId, request.RoleId);
            return Ok(result);
        }

        // DELETE: api/Roles
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole([FromRoute]string id)
        {
            var result = await _roleService.DeleteRole(id);
            return Ok(result);
        }
    }
}

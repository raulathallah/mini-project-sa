using LibraryManagementSystem.Application.Mappers;
using LibraryManagementSystem.Application.Service;
using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.Domain.Models.Requests.Users;
using LibraryManagementSystem.Domain.Service;
using LibraryManagementSystem.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryManagementSystem.WebApi.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UsersController>
        [Authorize(Roles ="Library Manager")]
        [HttpGet]
        public async Task<IEnumerable<object>> Get()
        {
            return await _userService.GetAllUser();
        }

        [Authorize(Roles ="Library User, Library Manager")]
        // GET api/<UsersController>/5
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get([FromRoute] int userId)
        {
            var user = await _userService.GetUserById(userId);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user.ToUserResponse());
        }

        // POST api/<UsersController>
        //[Authorize(Roles ="Library Manager")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddUserRequest request)
        {
            var user = await _userService.AddNewUser(request);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user.ToUserResponse());
        }

        // PUT api/<UsersController>/5
        [Authorize(Roles ="Library Manager")]
        [HttpPut("{userId}")]
        public async Task<IActionResult> Put([FromRoute]int userId, [FromBody] UpdateUserRequest request)
        {
            var user = await _userService.UpdateUser(userId, request);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user.ToUserResponse());
        }

        // DELETE api/<UsersController>/5
        [Authorize(Roles = "Library Manager")]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete([FromRoute]int userId)
        {
            var user = await _userService.DeleteUser(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user.ToUserResponse());
        }

        // GET api/<StocksController>/user-report
        //[Authorize(Roles = "Librarian, Library Manager")]
        [HttpGet("user-report/{appUserId}")]
        public async Task<IActionResult> GetUserReport([FromRoute] string appUserId)
        {
            var fileName = "UserReport.pdf";
            var response = await _userService.GenerateUserReportPDF(appUserId);
            return File(response, "application/pdf", fileName);

        }
    }
}

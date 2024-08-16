using LMS.Core.IRepositories;
using LMS.Core.Models;
using LMS.Core.Models.Requests;
using LMS.Domain.Models.Requests;
using LMS.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace LMS.WebApi.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _userRepository.GetAllUser();

        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] UpdateUserRequest user)
        {
            if (id < 1)
            {
                return BadRequest("Invalid user ID");
            }
            var response = await _userRepository.UpdateUser(id, user);
            if (response == null)
            {
                return NotFound("No user available");
            }

            return Ok(response);
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody] AddUserRequest user)
        {
            var response = await _userRepository.AddNewUser(user);
            if (response == null)
            {
                return BadRequest("No user available");
            }
            return Ok(response);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _userRepository.DeleteUser(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        /*        private bool UserExists(int id)
                {
                    return _context.Users.Any(e => e.Userid == id);
                }*/
    }

}

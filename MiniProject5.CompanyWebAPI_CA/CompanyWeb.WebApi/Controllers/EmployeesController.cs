
using CompanyWeb.Domain.Models.Dtos;
using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Helpers;
using CompanyWeb.Domain.Models.Requests;
using CompanyWeb.Domain.Models.Requests.Add;
using CompanyWeb.Domain.Services;
using CompanyWeb.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CompanyWeb.WebApi.Controllers
{
    public class EmployeesController : BaseController
    {
        private readonly IEmployeeService _employeeService;
        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Get all employees paging
        /// </summary>

        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     GET /Employees
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return all employee data </returns>
        // GET: api/Employees
        [HttpGet]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEmployees([FromQuery] int pageNumber, int perPage)
        {
            var response = await _employeeService.GetEmployees(pageNumber, perPage);
            if(response == null)
            {
                return null;
            }
            return Ok(response);
        }


        // NEW ======>
        /// <summary>
        /// Get all employees
        /// </summary>

        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     GET /Employees/all
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return all employee data </returns>
        // GET: api/Employees
        [HttpGet("all")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllEmployees()
        {
            var response = await _employeeService.GetAllEmployees();
            if (response == null)
            {
                return null;
            }
            return Ok(response);
        }


        /// <summary>
        /// Get employee data by ID
        /// </summary>

        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     GET /Employees/1
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return employee data by ID </returns>
        // GET: api/Employees/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Employee>> GetEmployee([FromRoute]int id)
        {
            var action = await _employeeService.GetEmployee(id);
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }


        /// <summary>
        /// Create employee
        /// </summary>

        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     POST /Employees
        ///     {
        ///         "fName": "Zahra",
        ///         "lName": "Safitri",
        ///         "address": "Jakarta, Indonesia",
        ///         "dob": "1998-08-20",
        ///         "sex": "Female",
        ///         "salary": 6000000,
        ///         "ssn": "081-71-XXXX",
        ///         "emailAddress": "zahra@gmail.com",
        ///         "phoneNumber": "082293019931",
        ///         "position": "Staff",
        ///         "deptNo": 1,
        ///         "empType": "Permanent",
        ///         "empLevel": 1,
        ///         "empDependents": [
        ///             {
        ///                 "fName": "Anton",
        ///                 "lName": "Syahputra",
        ///                 "sex": "Male",
        ///                 "birthDate": "2001-12-31",
        ///                 "relation": "Family"
        ///             }
        ///          ]
        ///      }
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return employee data that just created </returns>
        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPost]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostEmployee([FromBody] AddEmployeeRequest request)
        {
            var action = await _employeeService.CreateEmployee(request);
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }

        /// <summary>
        /// Update employee data by ID
        /// </summary>

        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     PUT /Employees/1
        ///     {
        ///         "fName": "Zahra",
        ///         "lName": "Safitri",
        ///         "address": "Jakarta, Indonesia",
        ///         "dob": "1998-08-20",
        ///         "sex": "Female",
        ///         "salary": 6000000,
        ///         "ssn": "081-71-XXXX",
        ///         "emailAddress": "zahra@gmail.com",
        ///         "phoneNumber": "082293019931",
        ///         "position": "Staff",
        ///         "deptNo": 1,
        ///         "empType": "Permanent",
        ///         "empLevel": 1,
        ///         "empDependents": [
        ///             {
        ///                 "fName": "Anton",
        ///                 "lName": "Syahputra",
        ///                 "sex": "Male",
        ///                 "birthDate": "2001-12-31",
        ///                 "relation": "Family"
        ///             }
        ///          ]
        ///      }
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return employee data that just updated </returns>
        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutEmployee([FromRoute]int id, [FromBody] UpdateEmployeeRequest request)
        {
            var action = await _employeeService.UpdateEmployee(id, request);
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }


        /// <summary>
        /// Delete employee by ID
        /// </summary>

        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     DELETE /Employees/1
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return employee data that just deleted </returns>
        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteEmployee([FromRoute]int id)
        {
            var action = await _employeeService.DeleteEmployee(id);
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }


        /// <summary>
        /// Search employee by parameters
        /// </summary>

        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     POST /Employees/search?searchBy=dept_keyword=IT_sortBy=name_isDescending=false
        ///     {
        ///         "pageNumber": 1,
        ///         "perPage": 30
        ///     }
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return employee data filtered by search parameters</returns>
        [HttpPost("search")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SearchEmployee([FromQuery] SearchEmployeeQuery query, [FromBody]PageRequest pageRequest)
        {
            var response = await _employeeService.SearchEmployee(query, pageRequest);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        /// <summary>
        /// Search employee by parameters
        /// </summary>

        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     PUT /Employees/14/deactivate
        ///     {
        ///         "deactivateReason": "Left"
        ///     }
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return employee data that just deactivate</returns>
        [HttpPut("{id}/deactivate")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeactivateEmployee([FromRoute] int id, [FromBody]DeactivateEmployeeRequest request)
        {
            var response = await _employeeService.DeactivateEmployee(id, request);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }
    }
}

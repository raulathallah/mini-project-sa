using CompanyWeb.Application.Repositories;
using CompanyWeb.Application.Services;
using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Requests;
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
        /// Get all employees
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
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees([FromQuery] int pageNumber, int perPage)
        {
            return await _employeeService.GetEmployees(pageNumber, perPage);
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
        ///         "fName": "Santino",
        ///         "lName": "Morello",
        ///         "address": "Cruce Casa de Postas 97, Spain",
        ///         "dob": "1975-02-22",
        ///         "sex": "Male",
        ///         "position": "Supervisor",
        ///         "deptNo": null
        ///     }
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
        public async Task<ActionResult<Employee>> PostEmployee([FromBody] AddEmployeeRequest request)
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
        ///         "fName": "Santino",
        ///         "lName": "Morello",
        ///         "address": "Cruce Casa de Postas 97, Spain",
        ///         "dob": "1975-02-22",
        ///         "sex": "Male",
        ///         "position": "Supervisor",
        ///         "deptNo": null
        ///     }
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
    }
}

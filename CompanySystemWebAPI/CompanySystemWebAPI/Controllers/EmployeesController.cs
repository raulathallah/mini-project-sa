using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanySystemWebAPI.Models;
using Asp.Versioning;
using CompanySystemWebAPI.Dtos.EmployeesAddDto;
using CompanySystemWebAPI.Interfaces;
using CompanySystemWebAPI.Services;

namespace CompanySystemWebAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _employeesService;

        public EmployeesController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
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
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees([FromQuery]int pageNumber, int perPage)
        {
            return await _employeesService.GetEmployees(pageNumber, perPage);
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
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var action = await _employeesService.GetEmployee(id);
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
        public async Task<IActionResult> PutEmployee(int id, [FromBody] EmployeesAddDto employee)
        {
            var action = await _employeesService.Update(id, employee);
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
        public async Task<ActionResult<Employee>> PostEmployee([FromBody] EmployeesAddDto employee)
        {
            var action = await _employeesService.Create(employee);
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
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var action = await _employeesService.Delete(id);
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }

        /// <summary>
        /// Get employee that born between 1980 - 1990
        /// </summary>

        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     GET /Employees/between-eighty-ninety
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return employee that born between 1980 - 1990 </returns>
        [HttpGet("between-eighty-ninety")]
        public async Task<IActionResult> GetEmployeeBetweenEightyAndNinety()
        {
            var action = await _employeesService.GetEmployeeBetweenEightyAndNinety();
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }

        /// <summary>
        /// Get employee that is female and born after 1990
        /// </summary>

        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     GET /Employees/between-eighty-ninety
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return employee that is female and born after 1990 </returns>
        [HttpGet("female-after-ninety")]
        public async Task<IActionResult> GetEmployeeFemaleAfterNinety()
        {
            var action = await _employeesService.GetEmployeeFemaleAfterNinety();
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }


        /// <summary>
        /// Get employee that is female manager in order
        /// </summary>

        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     GET /Employees/female-manager-in-order
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return employee that is female manager in order </returns>
        [HttpGet("female-manager-in-order")]
        public async Task<IActionResult> GetEmployeeFemaleManagerInOrder()
        {
            var action = await _employeesService.GetEmployeeFemaleManagerInOrder();
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }

        /// <summary>
        /// Get employee that is NOT manager
        /// </summary>

        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     GET /Employees/employee-not-manager
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return employee that is NOT manager </returns>
        [HttpGet("employee-not-manager")]
        public async Task<IActionResult> GetEmployeeNotManager()
        {
            var action = await _employeesService.GetEmployeeNotManager();
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }

        /// <summary>
        /// Get employee that is in IT Departement
        /// </summary>

        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     GET /Employees/employee-it
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return employee that is in IT Departement </returns>
        [HttpGet("employee-it")]
        public async Task<IActionResult> GetEmployeeIT()
        {
            var action = await _employeesService.GetEmployeeIT();
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }


        /// <summary>
        /// Get employee that is from Brazil, Russia, India, China and South Africa
        /// </summary>

        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     GET /Employees/employee-brics
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return employee that is from Brazil, Russia, India, China and South Africa </returns>
        [HttpGet("employee-brics")]
        public async Task<IActionResult> GetEmployeeBRICS()
        {
            var action = await _employeesService.GetEmployeeBRICS();
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }

        /// <summary>
        /// Get employee that is manager under age of 40
        /// </summary>

        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     GET /Employees/manager-under-fourty
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return employee that is manager under age of 40 </returns>
        [HttpGet("manager-under-fourty")]
        public async Task<IActionResult> GetManagerUnderFourty()
        {
            var action = await _employeesService.GetManagerUnderFourty();
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }

        /// <summary>
        /// Get employee that is female manager
        /// </summary>

        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     GET /Employees/manager-female
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return employee that is female manager </returns>
        [HttpGet("manager-female")]
        public async Task<IActionResult> GetManagerFemaleCount()
        {
            var action = await _employeesService.GetManagerFemaleCount();
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }

        /// <summary>
        /// Get employee that is retire this year
        /// </summary>

        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     GET /Employees/employee-retire
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return employee that is retire this year </returns>
        [HttpGet("employee-retire")]
        public async Task<IActionResult> GetEmployeeRetireThisYear()
        {
            var action = await _employeesService.GetEmployeeRetireThisYear();
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }

        /// <summary>
        /// Get employee list of ages
        /// </summary>

        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     GET /Employees/employee-ages
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return employee list of ages </returns>
        [HttpGet("employee-ages")]
        public async Task<IActionResult> GetEmployeeAges()
        {
            var action = await _employeesService.GetEmployeeAges();
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }

        /// <summary>
        /// Get employee that is not manager and supervisor
        /// </summary>

        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     GET /Employees/employee-not-manager-supervisor
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return employee that is not manager and supervisor </returns>
        [HttpGet("employee-not-manager-supervisor")]
        public async Task<IActionResult> GetEmployeeNotManagerAndSupervisor()
        {
            var action = await _employeesService.GetEmployeeNotManagerAndSupervisor();
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }
    }
}

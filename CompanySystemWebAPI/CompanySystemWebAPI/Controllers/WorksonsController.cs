using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanySystemWebAPI.Models;
using CompanySystemWebAPI.Interfaces;
using Asp.Versioning;
using CompanySystemWebAPI.Services;
using CompanySystemWebAPI.Dtos.WorksOnDto;

namespace CompanySystemWebAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]")]
    [ApiController]
    public class WorksonsController : ControllerBase
    {
        private readonly IWorksOnService _worksOnService;

        public WorksonsController(IWorksOnService worksOnService)
        {
            _worksOnService = worksOnService;
        }

        /// <summary>
        /// Get all work data with pagination
        /// </summary>

        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Worksons?pageNumber=1ANDperPage=20
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return all work data </returns>
        // GET: api/Worksons
        [HttpGet]
        [ProducesResponseType(typeof(Workson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Workson), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Workson>>> GetWorksons([FromQuery]int pageNumber, int perPage)
        {
            return await _worksOnService.GetWorksons(pageNumber, perPage);
        }

        /// <summary>
        /// Get work data by Project Number and Employee Number
        /// </summary>

        /// <remarks>
        /// 
        ///     /Worksons/_projectNumber_/_employeeNumber_    
        /// 
        /// Sample request:
        ///
        ///     GET /Worksons/11/3
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return work data by Project and Employee Number </returns>
        // GET: api/Worksons/5
        [HttpGet("{projNo}/{empNo}")]
        [ProducesResponseType(typeof(Workson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Workson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Workson), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Workson>> GetWorkson([FromRoute]int projNo, int empNo)
        {
            var workson = await _worksOnService.GetWorkson(projNo, empNo);
            if (workson == null)
            {
                return NotFound();
            }
            return workson;
        }



        /// <summary>
        /// Update work data by Project and Employee number
        /// </summary>

        /// <remarks>
        /// 
        ///     /Worksons/_projectNumber_/_employeeNumber_    
        /// 
        /// Sample request:
        ///
        ///     PUT /Worksons/11/3
        ///     {
        ///         "dateWorked": "2024-02-12",
        ///         "hoursWorked": 8
        ///     }
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return work data that just updated </returns>
        // PUT: api/Worksons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{projNo}/{empNo}")]
        [ProducesResponseType(typeof(Workson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Workson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Workson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutWorkson([FromRoute]int projNo, int empNo, [FromBody] WorksOnUpdateDto workson)
        {
            if (projNo < 1 || empNo < 1)
            {
                return BadRequest();
            }
            var action = await _worksOnService.Update(projNo, empNo, workson);
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }


        /// <summary>
        /// Create work
        /// </summary>

        /// <remarks>
        /// 
        /// 
        /// Sample request:
        ///
        ///     POST /Worksons
        ///     {
        ///         "empNo": 12,
        ///         "projNo": 13,
        ///         "dateWorked": "2024-02-12",
        ///         "hoursWorked": 8
        ///     }
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return work data that just created </returns>
        // POST: api/Worksons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(typeof(Workson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Workson), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Workson>> PostWorkson([FromBody] WorksOnAddDto workson)
        {
            var action = await _worksOnService.Create(workson);
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }


        /// <summary>
        /// Delete work by Project and Employee number
        /// </summary>

        /// <remarks>
        /// 
        /// 
        /// Sample request:
        ///
        ///     DELETE /Worksons/13/12
        ///     
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return work data that just deleted </returns>
        // DELETE: api/Worksons/5

        [HttpDelete("{projNo}/{empNo}")]
        [ProducesResponseType(typeof(Workson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Workson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Workson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteWorkson([FromRoute] int projNo, int empNo)
        {
            if (projNo < 1 || empNo < 1)
            {
                return BadRequest();
            }
            var action = await _worksOnService.Delete(projNo, empNo);
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }

        /// <summary>
        /// Get total hours worked for female employees
        /// </summary>

        /// <remarks>
        /// 
        /// 
        /// Sample request:
        ///
        ///     GET /Worksons/total-hours-female
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return departement name and number, and list of employee name with total works hour </returns>
        [HttpGet("total-hours-female")]
        public async Task<IActionResult> GetTotalHoursWorkedFemale()
        {
            var action = await _worksOnService.GetTotalHoursWorkedFemale();
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }

        /// <summary>
        /// Get total hours worked for every employee
        /// </summary>

        /// <remarks>
        /// 
        /// 
        /// Sample request:
        ///
        ///     GET /Worksons/total-hours
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return departement name and total works hour </returns>
        [HttpGet("total-hours")]
        public async Task<IActionResult> GetTotalHoursWorked()
        {
            var action = await _worksOnService.GetTotalHoursWorked();
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }
    }
}


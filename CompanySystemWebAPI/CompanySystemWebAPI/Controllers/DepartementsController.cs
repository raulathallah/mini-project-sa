using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanySystemWebAPI.Models;
using Asp.Versioning;
using CompanySystemWebAPI.Interfaces;
using CompanySystemWebAPI.Dtos.DepartementsDto;
using Npgsql.PostgresTypes;

namespace CompanySystemWebAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]")]
    [ApiController]
    public class DepartementsController : ControllerBase
    {
        private readonly IDepartementsService _departmentsService;

        public DepartementsController(IDepartementsService departmentsService)
        {
            _departmentsService = departmentsService;
        }

        /// <summary>
        /// Get all departements with paging
        /// </summary>

        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Departements?pageNumber=1ANDperPage=5
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return departement list with paging </returns>
        // GET: api/Departements
        [HttpGet]
        [ProducesResponseType(typeof(Departement),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Departement),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Departement),StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Departement>>> GetDepartements([FromQuery]int pageNumber, int perPage)
        {
            return await _departmentsService.GetDepartements(pageNumber, perPage);
        }

        /// <summary>
        /// Get departement by ID
        /// </summary>

        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Departements/1
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return departement data by ID </returns>
        // GET: api/Departements/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Departement), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Departement), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Departement), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Departement>> GetDepartement(int id)
        {
            var departement = await _departmentsService.GetDepartement(id);
            if (departement == null)
            {
                return NotFound();
            }
            return departement;
        }

        /// <summary>
        /// Update departement by ID
        /// </summary>

        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Departements/1
        ///     {
        ///         "deptName": "Finance",
        ///         "mgrEmpNo": 2
        ///     }
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return departement data that just updated </returns>
        // PUT: api/Departements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Departement), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Departement), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Departement), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutDepartement(int id, [FromBody] DepartementsAddDto departement)
        {
            if (id < 1)
            {
                return BadRequest();
            }
            var action = await _departmentsService.Update(id, departement);
            if(action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }

        /// <summary>
        /// Create departement
        /// </summary>

        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Departements
        ///     {
        ///         "deptName": "Finance",
        ///         "mgrEmpNo": null
        ///     }
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return departement data that just created </returns>
        // POST: api/Departements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(typeof(Departement), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Departement), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Departement>> PostDepartement([FromBody] DepartementsAddDto departement)
        {
            var action = await _departmentsService.Create(departement);
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }


        /// <summary>
        /// Delete departement by ID
        /// </summary>

        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /Departements/1
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return departement data that just deleted </returns>
        // DELETE: api/Departements/5
        [ProducesResponseType(typeof(Departement), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Departement), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Departement), StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartement(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }
            var action = await _departmentsService.Delete(id);
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }
    }
}

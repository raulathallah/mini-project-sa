using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Requests;
using CompanyWeb.Domain.Models.Requests.Add;
using CompanyWeb.Domain.Services;
using CompanyWeb.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyWeb.WebApi.Controllers
{
    public class WorksOnController : BaseController
    {
        private readonly IWorksOnService _worksOnService;

        public WorksOnController(IWorksOnService worksOnService)
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
        public async Task<ActionResult<IEnumerable<Workson>>> GetWorksons([FromQuery] int pageNumber, int perPage)
        {
            return await _worksOnService.GetWorksons(pageNumber, perPage);
        }

        //NEW======>
        /// <summary>
        /// Get all work data
        /// </summary>

        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Worksons?pageNumber=1ANDperPage=20
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return all work data </returns>
        // GET: api/Worksons/all
        [HttpGet("all")]
        [ProducesResponseType(typeof(Workson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Workson), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Workson>>> GetAllWorksons()
        {
            return await _worksOnService.GetAllWorksons();
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
        public async Task<ActionResult<Workson>> GetWorkson([FromRoute] int projNo, int empNo)
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
        public async Task<IActionResult> PutWorkson([FromRoute] int projNo, int empNo, [FromBody] UpdateWorksOnRequest workson)
        {
            if (projNo < 1 || empNo < 1)
            {
                return BadRequest();
            }
            var action = await _worksOnService.UpdateWorksOn(projNo, empNo, workson);
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
        public async Task<ActionResult<Workson>> PostWorkson([FromBody] AddWorksOnRequest workson)
        {
            var action = await _worksOnService.CreateWorksOn(workson);
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
            var action = await _worksOnService.DeleteWorksOn(projNo, empNo);
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }
    }
}

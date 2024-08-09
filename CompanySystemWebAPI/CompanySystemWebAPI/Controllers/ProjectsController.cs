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
using CompanySystemWebAPI.Dtos.ProjectsDto;
using CompanySystemWebAPI.Services;

namespace CompanySystemWebAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]")]
    public class ProjectsController : ControllerBase
    {

        private readonly IProjectsService _projectsService;

        public ProjectsController(IProjectsService projectsService)
        {
            _projectsService = projectsService;
        }


        /// <summary>
        /// Get all projects data
        /// </summary>

        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     GET /Projects
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return all project data </returns>
        // GET: api/Projects
        [HttpGet]
        [ProducesResponseType(typeof(Project), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Project), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects([FromQuery]int pageNumber, int perPage)
        {
            return await _projectsService.GetProjects(pageNumber, perPage);
        }

        /// <summary>
        /// Get project data by ID
        /// </summary>

        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     GET /Projects/1
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return project data by ID </returns>
        // GET: api/Projects/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Project), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Project), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Project), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _projectsService.GetProject(id);
            if (project == null)
            {
                return NotFound();
            }
            return project;
        }

        /// <summary>
        /// Update project by ID
        /// </summary>

        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     PUT /Projects/1
        ///     {
        ///         "projName": "Company Development 2024",
        ///         "deptNo": 2
        ///     }
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return project data that just updated </returns>
        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Project), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Project), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Project), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutProject(int id, [FromBody] ProjectsAddDto project)
        {
            if (id < 1)
            {
                return BadRequest();
            }
            var action = await _projectsService.Update(id, project);
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }

        /// <summary>
        /// Create project
        /// </summary>

        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     POST /Projects
        ///     {
        ///         "projName": "Company Development 2024",
        ///         "deptNo": 2
        ///     }
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return project data that just created </returns>
        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(typeof(Project), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Project), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Project), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Project>> PostProject([FromBody] ProjectsAddDto project)
        {
            var action = await _projectsService.Create(project);
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }

        /// <summary>
        /// Delete project
        /// </summary>

        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     DELETE /Projects/1
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return project data that just deleted </returns>
        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Project), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Project), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Project), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProject(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }
            var action = await _projectsService.Delete(id);
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }


        /// <summary>
        /// Get projects handled by Planning Departement
        /// </summary>

        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     GET /Projects/planning-departement-projects
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return all planning departement projects </returns>
        [HttpGet("planning-departement-projects")]
        public async Task<IActionResult> GetPlanningDepartementProjects()
        {
            var action = await _projectsService.GetPlanningDepartementProjects();
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }

        /// <summary>
        /// Get projects handled by IT and HR departements
        /// </summary>

        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     GET /Projects/it-hr-projects
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return all IT and HR departement projects </returns>
        [HttpGet("it-hr-projects")]
        public async Task<IActionResult> GetITAndHRProjects()
        {
            var action = await _projectsService.GetITAndHRProjects();
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }

        /// <summary>
        /// Get projects handled by female manager
        /// </summary>

        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     GET /Projects/female-manager-projects
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return all projects handled by female manager </returns>
        [HttpGet("female-manager-projects")]
        public async Task<IActionResult> GetFemaleManagerProjects()
        {
            var action = await _projectsService.GetFemaleManagerProjects();
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }

    }
}

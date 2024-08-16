﻿using CompanyWeb.Application.Repositories;
using CompanyWeb.Application.Services;
using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Requests;
using CompanyWeb.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CompanyWeb.WebApi.Controllers
{
    public class ProjectsController : BaseController
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
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
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects([FromQuery] int pageNumber, int perPage)
        {
            return await _projectService.GetProjects(pageNumber, perPage);
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
            var project = await _projectService.GetProject(id);
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
        public async Task<IActionResult> PutProject(int id, [FromBody] UpdateProjectRequest project)
        {
            if (id < 1)
            {
                return BadRequest();
            }
            var action = await _projectService.UpdateProject(id, project);

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
        public async Task<ActionResult<Project>> PostProject([FromBody] AddProjectRequest project)
        {
            var action = await _projectService.CreateProject(project);
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
            var action = await _projectService.DeleteProject(id);
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }

    }
}

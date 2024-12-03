
using CompanyWeb.Application.Services;
using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Requests;
using CompanyWeb.Domain.Models.Requests.Add;
using CompanyWeb.Domain.Services;
using CompanyWeb.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Administrator, HR Manager, Department Manager, Employee Supervisor, Employee")]
        [HttpGet]
        [ProducesResponseType(typeof(Project), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Project), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProjects([FromQuery] int pageNumber, int perPage)
        {
            return Ok(await _projectService.GetProjects(pageNumber, perPage));
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
        [Authorize(Roles = "Administrator, HR Manager, Employee, Department Manager, Employee Supervisor")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Project), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Project), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Project), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProject(int id)
        {
            var project = await _projectService.GetProject(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [Authorize(Roles = "Administrator, HR Manager, Employee, Department Manager, Employee Supervisor")]
        [HttpGet("all")]
        [ProducesResponseType(typeof(Project), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Project), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Project), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllProject()
        {
            var project = await _projectService.GetAllProject();
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
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
        ///         "projLocation": 1,
        ///         "deptNo": 2
        ///     }
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return project data that just updated </returns>
        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrator, Department Manager")]
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
        ///         "deptNo": 2,
        ///         "projLocation": 1,
        ///     }
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return project data that just created </returns>
        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrator, Department Manager")]
        [HttpPost]
        [ProducesResponseType(typeof(Project), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Project), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Project), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostProject([FromBody] AddProjectRequest project)
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
        [Authorize(Roles = "Administrator, Department Manager")]
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

        // PROJECT REPORT PDF
        [HttpGet("report-pdf")]
        public async Task<IActionResult> GetProjectReportPDF()
        {
            var fileName = "ProjectReport.pdf";
            var response = await _projectService.GenerateProjectReportPDF();
            return File(response, "application/pdf", fileName);
        }

        // PROJECT REPORT JSON
        [HttpGet("report")]
        public async Task<IActionResult> GetProjectReport()
        {
            var action = await _projectService.GetProjectReport();
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }
    }
}

using CompanyWeb.Domain.Services;
using CompanyWeb.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CompanyWeb.WebApi.Controllers
{
    public class CompanyController : BaseController
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        /// <summary>
        /// Get employee that born between 1980 - 1990
        /// </summary>

        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     GET /Company/between-eighty-ninety
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return employee that born between 1980 - 1990 </returns>
        [HttpGet("between-eighty-ninety")]
        public async Task<IActionResult> GetEmployeeBetweenEightyAndNinety()
        {
            var action = await _companyService.GetEmployeeBetweenEightyAndNinety();
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
        ///     GET /Company/between-eighty-ninety
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return employee that is female and born after 1990 </returns>
        [HttpGet("female-after-ninety")]
        public async Task<IActionResult> GetEmployeeFemaleAfterNinety()
        {
            var action = await _companyService.GetEmployeeFemaleAfterNinety();
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
        ///     GET /Company/female-manager-in-order
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return employee that is female manager in order </returns>
        [HttpGet("female-manager-in-order")]
        public async Task<IActionResult> GetEmployeeFemaleManagerInOrder()
        {
            var action = await _companyService.GetEmployeeFemaleManagerInOrder();
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
        ///     GET /Company/employee-not-manager
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return employee that is NOT manager </returns>
        [HttpGet("employee-not-manager")]
        public async Task<IActionResult> GetEmployeeNotManager()
        {
            var action = await _companyService.GetEmployeeNotManager();
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
        ///     GET /Company/employee-it
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return employee that is in IT Departement </returns>
        [HttpGet("employee-it")]
        public async Task<IActionResult> GetEmployeeIT()
        {
            var action = await _companyService.GetEmployeeIT();
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
        ///     GET /Company/employee-brics
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return employee that is from Brazil, Russia, India, China and South Africa </returns>
        [HttpGet("employee-brics")]
        public async Task<IActionResult> GetEmployeeBRICS()
        {
            var action = await _companyService.GetEmployeeBRICS();
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
        ///     GET /Company/manager-under-fourty
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return employee that is manager under age of 40 </returns>
        [HttpGet("manager-under-fourty")]
        public async Task<IActionResult> GetManagerUnderFourty()
        {
            var action = await _companyService.GetManagerUnderFourty();
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
        ///     GET /Company/manager-female
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return employee that is female manager </returns>
        [HttpGet("manager-female")]
        public async Task<IActionResult> GetManagerFemaleCount()
        {
            var action = await _companyService.GetManagerFemaleCount();
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
        ///     GET /Company/employee-retire
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return employee that is retire this year </returns>
        [HttpGet("employee-retire")]
        public async Task<IActionResult> GetEmployeeRetireThisYear()
        {
            var action = await _companyService.GetEmployeeRetireThisYear();
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
        ///     GET /Company/employee-ages
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return employee list of ages </returns>
        [HttpGet("employee-ages")]
        public async Task<IActionResult> GetEmployeeAges()
        {
            var action = await _companyService.GetEmployeeAges();
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
        ///     GET /Company/employee-not-manager-supervisor
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return employee that is not manager and supervisor </returns>
        [HttpGet("employee-not-manager-supervisor")]
        public async Task<IActionResult> GetEmployeeNotManagerAndSupervisor()
        {
            var action = await _companyService.GetEmployeeNotManagerAndSupervisor();
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
        ///     GET /Company/planning-departement-projects
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return all planning departement projects </returns>
        [HttpGet("planning-departement-projects")]
        public async Task<IActionResult> GetPlanningDepartementProjects()
        {
            var action = await _companyService.GetPlanningDepartementProjects();
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
        ///     GET /Company/it-hr-projects
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return all IT and HR departement projects </returns>
        [HttpGet("it-hr-projects")]
        public async Task<IActionResult> GetITAndHRProjects()
        {
            var action = await _companyService.GetITAndHRProjects();
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
        ///     GET /Company/female-manager-projects
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return all projects handled by female manager </returns>
        [HttpGet("female-manager-projects")]
        public async Task<IActionResult> GetFemaleManagerProjects()
        {
            var action = await _companyService.GetFemaleManagerProjects();
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
        ///     GET /Company/total-hours-female
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return departement name and number, and list of employee name with total works hour </returns>
        [HttpGet("total-hours-female")]
        public async Task<IActionResult> GetTotalHoursWorkedFemale()
        {
            var action = await _companyService.GetTotalHoursWorkedFemale();
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
        ///     GET /Company/total-hours
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return departement name and total works hour </returns>
        [HttpGet("total-hours")]
        public async Task<IActionResult> GetTotalHoursWorked()
        {
            var action = await _companyService.GetTotalHoursWorked();
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }

        /// <summary>
        /// Get retirement status employee
        /// </summary>

        /// <remarks>
        /// 
        /// 
        /// Sample request:
        ///
        ///     GET /Company/retirement-status
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return departement name and total works hour </returns>
        [HttpGet("project-no-employee")]
        public async Task<IActionResult> GetAllProjectWithoutEmployee()
        {
            var action = await _companyService.GetAllProjectWithoutEmployee();
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }

        /// <summary>
        /// Get departement more than 10 employee
        /// </summary>

        /// <remarks>
        /// 
        /// 
        /// Sample request:
        ///
        ///     GET /Company/departement-more-ten
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return departement name and total works hour </returns>
        [HttpGet("departement-more-ten")]
        public async Task<IActionResult> GetDepartementMoreThanTenEmployee()
        {
            var action = await _companyService.GetDepartementMoreThanTenEmployee();
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }

        /// <summary>
        /// Get min and max hours for each employee
        /// </summary>

        /// <remarks>
        /// 
        /// 
        /// Sample request:
        ///
        ///     GET /Company/hours-min-max
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return departement name and total works hour </returns>
        [HttpGet("hours-min-max")]
        public async Task<IActionResult> GetMinMaxHoursWorked()
        {
            var action = await _companyService.GetMinMaxHoursWorked();
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }

        /// <summary>
        /// Get employee work detail
        /// </summary>

        /// <remarks>
        /// 
        /// 
        /// Sample request:
        ///
        ///     GET /Company/employee-work-detail
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return departement name and total works hour </returns>
        [HttpGet("employee-work-detail")]
        public async Task<IActionResult> GetEmployeeWorkDetail()
        {
            var action = await _companyService.GetEmployeeWorkDetail();
            if (action == null)
            {
                return NotFound();
            }
            return Ok(action);
        }
    }
}


using CompanyWeb.Domain.Models.Auth;
using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Options;
using CompanyWeb.Domain.Models.Requests;
using CompanyWeb.Domain.Repositories;
using CompanyWeb.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartementRepository _departementRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IWorksOnRepository _worksOnRepository;
        private readonly IWorkflowRepository _workflowRepository;
        private readonly CompanyOptions _companyOptions;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CompanyService( 
            IEmployeeRepository employeeRepository, 
            IDepartementRepository departementRepository,
            IProjectRepository projectRepository,
            IWorksOnRepository worksOnRepository,
            IOptions<CompanyOptions> companyOptions,
            IWorkflowRepository workflowRepository,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _employeeRepository = employeeRepository;
            _departementRepository = departementRepository;
            _projectRepository = projectRepository;
            _worksOnRepository = worksOnRepository;
            _companyOptions = companyOptions.Value;
            _workflowRepository = workflowRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<object>> GetEmployeeBetweenEightyAndNinety()
        {
            var emp = await _employeeRepository.GetAllEmployees();
            var result = emp
                .Where(x => x.Dob.Year > 1980 && x.Dob.Year < 1990)
                .ToList<object>();
            return result;
        }

        public async Task<List<object>> GetEmployeeFemaleAfterNinety()
        {
            var emp = await _employeeRepository.GetAllEmployees();
            var result = emp
                .Where(x => x.Sex == "Female" && x.Dob.Year > 1990)
                .ToList<object>();
            return result;
        }

        public async Task<List<object>> GetEmployeeFemaleManagerInOrder()
        {
            var employees = await _employeeRepository.GetAllEmployees();
            var departements = await _departementRepository.GetAllDepartements();

            var result = (from value in employees
                                join dept in departements on value.Deptno equals dept.Deptno
                                where value.Sex == "Female" && dept.Mgrempno == value.Empno
                                select value)
                                .Select(s => new
                                {
                                    Empno = s.Empno,
                                    Empname = s.Fname + " "+ s.Lname,
                                    Deptno = s.Deptno,
                                    Deptname = s.DeptnoNavigation.Deptname,
                                    Sex = s.Sex
                                })
                                .ToList<object>();
                                
            return result;
        }

        public async Task<List<object>> GetEmployeeNotManager()
        {
            var employees = await _employeeRepository.GetAllEmployees();
            var departements = await _departementRepository.GetAllDepartements();

            var emp = from value in employees
                      join dept in departements on value.Empno equals dept.Mgrempno
                      select value;

            var result = employees
                .Except(emp)
                .Select(s=>new
                {
                    Empno = s.Empno,
                    Empname = s.Fname + " " + s.Lname,
                    Position = s.Position,
                    Deptno = s.Deptno
                })
                .ToList<object>();
            return result;
        }

        public async Task<List<object>> GetEmployeeIT()
        {

            var departements = await _departementRepository.GetAllDepartements();
            var employees = await _employeeRepository.GetAllEmployees();

            var deptId = departements.SingleOrDefault(s => s.Deptname == "IT")?.Deptno;
            if (deptId == null)
            {
                return null;
            }

            var result = employees
                .Where(w => w.Deptno == deptId)
                .Select(s => new
                {
                    Name = s.Fname + " " + s.Lname,
                    Departement = s.DeptnoNavigation.Deptname,
                    Address = s.Address,
                })
                .ToList<object>();
            return result;
        }

        public async Task<List<object>> GetEmployeeBRICS()
        {
            var employees = await _employeeRepository.GetAllEmployees();
            string[] brics = { "Brazil", "Russia", "India", "China", "South Africa" };

            var result = employees
                .Where(w => brics.Any(any => w.Address.Contains(any)))
                .Select(s => new
                {
                    Name = s.Fname + " " + s.Lname,
                    Address = s.Address,
                })
                .ToList<object>();
            return result;
        }
        public async Task<List<object>> GetManagerUnderFourty()
        {
            var employees = await _employeeRepository.GetAllEmployees();
            var departements = await _departementRepository.GetAllDepartements();

            var emp = from value in employees
                      join dept in departements on value.Empno equals dept.Mgrempno
                      select value;

            var result = employees
                .Where(w => GetYearNow() - w.Dob.Year < 40)
                .Select(s => new
                {
                    Name = s.Fname + " " + s.Lname,
                    Age = GetYearNow() - s.Dob.Year,
                    Address = s.Address,
                })
                .ToList<object>();
            return result;
        }

        public async Task<int> GetManagerFemaleCount()
        {
            var employees = await _employeeRepository.GetAllEmployees();
            var departements = await _departementRepository.GetAllDepartements();

            var result = await (from value in employees
                                join dept in departements on value.Empno equals dept.Mgrempno
                      where value.Sex == "Female"
                      select value).CountAsync();
            return result;
        }

        public async Task<List<object>> GetEmployeeRetireThisYear()
        {
            var employees = await _employeeRepository.GetAllEmployees();

            var emp = await employees
                .Where(w => GetYearNow() - w.Dob.Year >= _companyOptions.MaxEmployeeAge)
                .Select(s => new
                {
                    Nama = s.Fname,
                    Age = GetYearNow() - s.Dob.Year
                })
                .ToListAsync<object>();
            return emp;
        }

        public async Task<List<object>> GetEmployeeAges()
        {
            var employees = await _employeeRepository.GetAllEmployees();

            var emp = await employees
                .Select(s => new
                {
                    Name = s.Fname + " " + s.Lname,
                    Departement = s.DeptnoNavigation.Deptname,
                    Age = GetYearNow() - s.Dob.Year,
                })
                .ToListAsync<object>();

            return emp;
        }

        public async Task<List<object>> GetEmployeeNotManagerAndSupervisor()
        {
            var employees = await _employeeRepository.GetAllEmployees();
            var departements = await _departementRepository.GetAllDepartements();

            var emp = from value in employees
                      join dept in departements on value.Empno equals dept.Mgrempno
                      select value;

            var result = await employees
                .Except(emp)
                .Where(w => !w.Position.Contains("Supervisor"))
                .Select(s => new
                {
                    FirstName = s.Fname,
                    LastName = s.Lname,
                    Position = s.Position,
                    Sex = s.Sex,
                    Deptno = s.Deptno,
                })
                .ToListAsync<object>();
            return result;
        }

        public async Task<List<object>> GetPlanningDepartementProjects()
        {
            var projects = await _projectRepository.GetAllProjects();
            var departemetns = await _departementRepository.GetAllDepartements();

            var proj = await (from value in projects
                              join dept in departemetns on value.Deptno equals dept.Deptno
                              where dept.Deptname == "Planning"
                              select value)
                              .ToListAsync<object>();

            return proj;
        }
        public async Task<List<object>> GetITAndHRProjects()
        {
            var projects = await _projectRepository.GetAllProjects();
            var departemetns = await _departementRepository.GetAllDepartements();

            var proj = await (from value in projects
                              join dept in departemetns on value.Deptno equals dept.Deptno
                              where (dept.Deptname == "IT" || dept.Deptname == "HR")
                              select value)
                       .ToListAsync<object>();
            return proj;
        }
        public async Task<List<object>> GetFemaleManagerProjects()
        {
            var employees = await _employeeRepository.GetAllEmployees();
            var departemetns = await _departementRepository.GetAllDepartements();
            var projects = await _projectRepository.GetAllProjects();

            var projGroup = from value in employees
                            join dept in departemetns on value.Empno equals dept.Mgrempno
                           join proj in projects on dept.Deptno equals proj.Deptno
                           where (value.Sex == "Female")
                           group proj by value.Empno;

            var result = await projGroup
                .Select(s => new
                {
                    Name = s
                    .Select(n => n.DeptnoNavigation.MgrempnoNavigation.Fname + " " + n.DeptnoNavigation.MgrempnoNavigation.Lname)
                    .FirstOrDefault(),
                    Projects = s.ToList(),
                })
                .ToListAsync<object>();

            return result;
        }

        public async Task<List<object>> GetTotalHoursWorkedFemale()
        {
            var employees = await _employeeRepository.GetAllEmployees();
            var workson = await _worksOnRepository.GetAllWorksons();

            var total = from emp in employees
                        join work in workson on emp.Empno equals work.Empno
                        where (emp.Sex == "Female")
                        orderby (emp.Lname)
                        group work by emp.Deptno;

            var result = await total
                         .Select(s => new
                         {
                             DeptNo = s.Key,
                             Employee = s
                             .GroupBy(gb => gb.Empno)
                             .Select(e => new
                             {
                                 Name = e
                                 .Select(n => n.EmpnoNavigation.Fname + " " + n.EmpnoNavigation.Lname)
                                 .Distinct()
                                 .FirstOrDefault(),
                                 TotalHours = e
                                 .Select(th => th.Hoursworked).Sum(),
                             }),
                         })
                         .ToListAsync<object>();

            return result;
        }

        public async Task<List<object>> GetTotalHoursWorked()
        {
            var employees = await _employeeRepository.GetAllEmployees();
            var workson = await _worksOnRepository.GetAllWorksons();
            var result = await (from emp in employees
                                join work in workson on emp.Empno equals work.Empno
                                group work by work.Empno)
                        .Select(s => new
                        {
                            Name = s
                            .Select(n => n.EmpnoNavigation.Fname + " " + n.EmpnoNavigation.Lname)
                            .FirstOrDefault(),
                            TotalHours = s
                            .Select(th => th.Hoursworked).Sum(),
                        })
                        .OrderBy(ob => ob.Name)
                        .ToListAsync<object>();


            return result;
        }

        public async Task<List<object>> GetAllProjectWithoutEmployee()
        {
            var projects = await _projectRepository.GetAllProjects();
            var workson = await _worksOnRepository.GetAllWorksons();

            var result = await projects
                .Where(p => !workson.Any(any => any.Projno == p.Projno))
                .ToListAsync<object>();
            return result;
        }

        public async Task<List<object>> GetDepartementMoreThanTenEmployee()
        {
            var employees = await _employeeRepository.GetAllEmployees();
            var departements = await _departementRepository.GetAllDepartements();
            var result = await (from emp in employees
                                join dept in departements on emp.Deptno equals dept.Deptno
                                group emp by emp.Deptno)
                                .Select(s => new
                                {
                                    Deptno = s.Key,
                                    EmpCount = s.Count(),
                                })
                                .Where(w=>w.EmpCount > 10)
                                .OrderBy(ob => ob.Deptno)
                                .ToListAsync<object>();

            return result;
        }

        public async Task<List<object>> GetMinMaxHoursWorked()
        {
            var workson = await _worksOnRepository.GetAllWorksons();
            var result = await workson
                .GroupBy(gb => gb.Empno)
                .Select(s => new
                {
                    Empno = s.Key,
                    Name = s.Select(x => x.EmpnoNavigation.Fname + " " + x.EmpnoNavigation.Lname).FirstOrDefault(),
                    Min = s.Select(x => x.Hoursworked).Min(),
                    Max = s.Select(x => x.Hoursworked).Max()
                })
                .ToListAsync<object>();


            return result;
        }

        public async Task<List<object>> GetEmployeeWorkDetail()
        {
            var workson = await _worksOnRepository.GetAllWorksons();
            var result = await workson
                .GroupBy(gb => gb.Empno)
                .Select(s => new
                {
                    Empno = s.Key,
                    Name = s.Select(x => x.EmpnoNavigation.Fname + " " + x.EmpnoNavigation.Lname).FirstOrDefault(),
                    Project = s.Select(x => new
                    {
                        Projno = x.Projno,
                        Projname = x.ProjnoNavigation.Projname,
                    }),
                    TotalHoursWorked = s.Select(x => x.Hoursworked).Sum()
                })
                .ToListAsync<object>();
            return result;
        }

        // DASHBOARD
        public async Task<object> GetCompanyDashboard()
        {
            var employees = await _employeeRepository.GetAllEmployees();
            var departments = await _departementRepository.GetAllDepartements();
            var workson = await _worksOnRepository.GetAllWorksons();

            var d = (from value in employees
                     join dept in departments on value.Deptno equals dept.Deptno
                     select value)
                    .GroupBy(gb => gb.DeptnoNavigation.Deptname)
                    .Select(s => new
                    {
                        Department = s.Key,
                        EmpCount = s.Count()
                    });

            var avgSalary = (from value in employees
                             join dept in departments on value.Deptno equals dept.Deptno
                             select value)
                    .GroupBy(gb => gb.DeptnoNavigation.Deptname)
                    .Select(s => new
                    {
                        Department = s.Key,
                        AvgSalary = Math.Round(s.Select(s2 => s2.Salary).Average(), 2)
                    });

            var totalHoursByEmpDesc = workson.GroupBy(gb => gb.Empno)
                .Select(s => new
                {
                    Empno = s.Key,
                    EmpName = s.Select(s => s.EmpnoNavigation.Fname + " " + s.EmpnoNavigation.Lname).FirstOrDefault(),
                    TotalHoursworked = s.Select(x => x.Hoursworked).Sum()
                })
                .OrderByDescending(ob=>ob.TotalHoursworked)
                .Take(5)
                .ToList();

            return new
            {
                EmpDistribution = d,
                DeptAvgSalary = avgSalary,
                EmpTopPerformance = totalHoursByEmpDesc
            };
        }

        // WORKFLOW DASHBOARD
        public async Task<List<object>> GetWorkflowDashboard()
        {
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var role = await _userManager.GetRolesAsync(user);
            List<string> roleId = new();
            foreach (var r in role)
            {
                var appRole = await _roleManager.FindByNameAsync(r);
                roleId.Add(appRole.Id);
            }
            var ws = await _workflowRepository.GetAllWorkflowSequence();
            var wf = await _workflowRepository.GetAllWorkflow();

            var userStepId = ws.Where(w => roleId.Any(a => a == w.RequiredRole)).Select(s => s.StepId).FirstOrDefault();
            var allProcess = await _workflowRepository.GetAllProcess();
            var userProcess = allProcess.Where(w => w.CurrentStepId == userStepId).ToList();

            List<object> result = new List<object>();
            foreach (var value in userProcess)
            {
                var wfName = wf.Where(w => w.WorkflowId == value.WorkflowId).Select(s => s.WorkflowName).FirstOrDefault();
                var stepName = ws.Where(w => w.StepId == value.CurrentStepId).Select(s => s.StepName).FirstOrDefault();
                var requester = await _employeeRepository.GetEmployee(value.Empno);
                result.Add(new
                {
                    ProcessId = value.ProcessId,
                    Workflow = wfName,
                    Requester = requester.Fname + " " + requester.Lname,
                    RequestDate = value.RequestDate,
                    Status = value.Status,
                    CurrentStep = stepName,
                });
            }
            return result;
        }

        //extra
        static int GetYearNow()
        {
            return DateOnly.FromDateTime(DateTime.Now).Year;
        }

    
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Services
{
    public interface ICompanyService
    {
        // a.List all employees from BRICS(Brazil, Russia, India, China, South Africa) countries in alphabetical order of surname.
        Task<List<object>> GetEmployeeBRICS();

        // b.List all the details of employees born between 1980–1990.
        Task<List<object>> GetEmployeeBetweenEightyAndNinety();

        // c.List all the details of employees who are female and born after 1990.
        Task<List<object>> GetEmployeeFemaleAfterNinety();

        // d.List all managers who are female in alphabetical order of surname, and then first name.
        Task<List<object>> GetEmployeeFemaleManagerInOrder();

        // e.List all employees who are not managers
        Task<List<object>> GetEmployeeNotManager();

        // f.List all projects that are managed by the planning department.
        Task<List<object>> GetPlanningDepartementProjects();

        // g.List all projects which had no employees working.
        Task<List<object>> GetAllProjectWithoutEmployee();

        // h.List the total number of employees in each department for those departments with more than 10 employees.
        Task<List<object>> GetDepartementMoreThanTenEmployee();

        // i.Produce a list of the names and addresses of all employees who work for the IT department.
        Task<List<object>> GetEmployeeIT();

        // j.Produce a complete list of all managers who are due to retire this year, in alphabetical order of surname.
        Task<List<object>> GetEmployeeRetireThisYear();

        // k.Produce a report of the total hours worked by each female employee, arranged by department number and alphabetically by employee surname within each department.
        Task<List<object>> GetTotalHoursWorkedFemale();

        // l.Find out how many managers are female.
        Task<int> GetManagerFemaleCount();

        // m.List all projects that are managed by the IT and the HR department.
        Task<List<object>> GetITAndHRProjects();

        // n. Retrieve the list of employees who are neither managers nor supervisors. Attributes to be retrieved are first name, last name, position, sex and department number
        Task<List<object>> GetEmployeeNotManagerAndSupervisor();


        // o.Retrieve all the female manager and the project that she managed
        Task<List<object>> GetFemaleManagerProjects();

        // p.Retrieve the maximum and minimum hours worked by employee
        Task<List<object>> GetMinMaxHoursWorked();


        // q.Retrieve the total hours worked for each employee
        Task<List<object>> GetTotalHoursWorked();


        // r.Calculate employee age, show employee name, department and their age
        Task<List<object>> GetEmployeeAges();

        // s.Calculate total hours worked by employee, show employee name, project name and their total hours worked
        Task<List<object>> GetEmployeeWorkDetail();


        // t.Show all the managers that under 40
        Task<List<object>> GetManagerUnderFourty();


        // Company Dashboard
        Task<object> GetCompanyDashboard();

        // Workflow Dashboard
        Task<List<object>> GetWorkflowDashboard();
    }
}

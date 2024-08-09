using CompanySystemWebAPI.Dtos.EmployeesAddDto;
using CompanySystemWebAPI.Models;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;

namespace CompanySystemWebAPI.Interfaces
{
    public interface IEmployeesService
    {
        Task<Employee> Create(EmployeesAddDto emp);
        Task<Employee> Update(int id, EmployeesAddDto emp);
        Task<Employee> Delete(int id);
        Task<List<Employee>> GetEmployees(int pageNumber, int perPage);
        Task<Employee> GetEmployee(int id);

        // b.List all the details of employees born between 1980–1990.
        Task<List<object>> GetEmployeeBetweenEightyAndNinety();

        // c.List all the details of employees who are female and born after 1990.
        Task<List<object>> GetEmployeeFemaleAfterNinety();

        // d.List all managers who are female in alphabetical order of surname, and then first name.
        Task<List<object>> GetEmployeeFemaleManagerInOrder();

        // e.List all employees who are not managers
        Task<List<object>> GetEmployeeNotManager();

        // i.Produce a list of the names and addresses of all employees who work for the IT department.
        Task<List<object>> GetEmployeeIT();

        // a.List all employees from BRICS(Brazil, Russia, India, China, South Africa) countries in alphabetical order of surname.
        Task<List<object>> GetEmployeeBRICS();

        // t.Show all the managers that under 40
        Task<List<object>> GetManagerUnderFourty();

        // l.Find out how many managers are female.
        Task<int> GetManagerFemaleCount();

        // j.Produce a complete list of all managers who are due to retire this year, in alphabetical order of surname.
        Task<List<object>> GetEmployeeRetireThisYear();

        // r.Calculate employee age, show employee name, department and their age
        Task<List<object>> GetEmployeeAges();

        // n. Retrieve the list of employees who are neither managers nor supervisors. Attributes to be retrieved are first name, last name, position, sex and department number
        Task<List<object>> GetEmployeeNotManagerAndSupervisor();

    }
}

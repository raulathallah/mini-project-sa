using CompanyWeb.Domain.Models.Dtos;
using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Requests;
using CompanyWeb.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> Create(Employee employee);
        Task<Employee> Update(Employee employee);
        Task<Employee> Delete(Employee employee);
        Task<List<Employee>> GetEmployees(int pageNumber, int perPage);
        Task<IQueryable<Employee>> GetAllEmployees();
        Task<Employee> GetEmployee(int id);
    }
}

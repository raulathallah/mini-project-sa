using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Requests;
using CompanyWeb.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Application.Services
{
    public interface IEmployeeService
    {
        Task<object> CreateEmployee(AddEmployeeRequest request);
        Task<List<Employee>> GetEmployees(int pageNumber, int perPage);
        Task<Employee> GetEmployee(int id);
        Task<Employee> UpdateEmployee(int id, UpdateEmployeeRequest request);
        Task<Employee> DeleteEmployee(int id);
    }
}

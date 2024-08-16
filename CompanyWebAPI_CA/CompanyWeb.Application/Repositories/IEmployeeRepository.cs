using CompanyWeb.Domain.Models.Dtos;
using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Requests;
using CompanyWeb.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Application.Repositories
{
    public interface IEmployeeRepository
    {
        Task<EmployeeDetailDto> Create(AddEmployeeRequest request);
        Task<Employee> Update(int id, UpdateEmployeeRequest request);
        Task<Employee> Delete(int id);
        Task<List<Employee>> GetEmployees(int pageNumber, int perPage);
        Task<IQueryable<Employee>> GetAllEmployees();
        Task<Employee> GetEmployee(int id);
    }
}

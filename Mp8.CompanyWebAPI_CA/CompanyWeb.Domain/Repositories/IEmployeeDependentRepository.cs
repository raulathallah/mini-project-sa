using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Repositories
{
    public interface IEmployeeDependentRepository
    {
        Task<EmployeeDependent> Create(EmployeeDependent employeeDependent);
        Task<List<EmployeeDependent>> Delete(int empNo);
        Task<IQueryable<EmployeeDependent>> GetAllEmployeeDependents();
        // get by empno //
        Task<List<EmployeeDependent>> GetEmployeeDependentByEmpNo(int empNo);
    }
}

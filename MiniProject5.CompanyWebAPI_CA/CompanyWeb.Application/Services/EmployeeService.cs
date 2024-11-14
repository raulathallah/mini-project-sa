using CompanyWeb.Application.Mappers;
using CompanyWeb.Domain.Models.Dtos;
using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Helpers;
using CompanyWeb.Domain.Models.Options;
using CompanyWeb.Domain.Models.Requests;
using CompanyWeb.Domain.Models.Requests.Add;
using CompanyWeb.Domain.Models.Responses.Employee;
using CompanyWeb.Domain.Repositories;
using CompanyWeb.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CompanyWeb.Application.Services
{
    public class EmployeeService :  IEmployeeService
    {
        private readonly ICompanyService _companyService;
        private readonly IDepartementRepository _departementRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeDependentRepository _employeeDependentRepository;
        private readonly CompanyOptions _companyOptions;

        public EmployeeService(ICompanyService companyService, 
            IDepartementRepository departementRepository, 
            IEmployeeRepository employeeRepository, 
            IEmployeeDependentRepository employeeDependentRepository, 
            IOptions<CompanyOptions> companyOptions)
        {
            _companyService = companyService;
            _departementRepository = departementRepository;
            _employeeRepository = employeeRepository;
            _employeeDependentRepository = employeeDependentRepository;
            _companyOptions = companyOptions.Value;
        }

        public async Task<object> CreateEmployee(AddEmployeeRequest request)
        {
            var response = CreateResponse();
            var newEmp = new Employee()
            {
                Fname = request.Fname,
                Lname = request.Lname,
                Dob = request.Dob,
                Sex = request.Sex,
                Address = request.Address,
                EmailAddress = request.EmailAddress,
                PhoneNumber = request.PhoneNumber,
                Position = request.Position,
                Deptno = request.Deptno,
                EmpLevel = request.EmpLevel,
                EmpType = request.EmpType,
                //NEW======>
                DirectSupervisor = request.DirectSupervisor,
                Ssn = request.Ssn,
                Salary = request.Salary,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
            };
            int deptId = await _departementRepository.GetDepartementIdByName("IT");
            if(request.Deptno == deptId)
            {
                int memberCount = _companyService.GetEmployeeIT().Result.Count;
                if (memberCount >= _companyOptions.MaxDepartementMemberIT)
                {
                    response.Message = $"IT Employee full ({memberCount}/9). Please try again";
                    return response;
                }
            }
            
            var data = await _employeeRepository.Create(newEmp);

            foreach (var item in request.EmpDependents)
            {
                var newEmpDependent = new EmployeeDependent()
                {
                    DependentEmpno = data.Empno,
                    BirthDate = item.BirthDate,
                    Fname = item.Fname,
                    Lname = item.Lname,
                    Sex = item.Sex,
                    Relation = item.Relation
                };
                await _employeeDependentRepository.Create(newEmpDependent);
            }

            var dependents = await _employeeDependentRepository.GetEmployeeDependentByEmpNo(data.Empno);

            response.Status = true;
            response.Message = "Success";
            response.Data = data.ToEmployeeResponse(dependents);
            return response;
        }

        public async Task<object> DeactivateEmployee(int id, DeactivateEmployeeRequest request)
        {
            var emp = await _employeeRepository.GetEmployee(id);
            if(emp == null || emp.IsActive == false)
            {
                return null;
            }
            if(emp.IsActive == true)
            {
                emp.IsActive = false;
                emp.UpdatedAt = DateTime.UtcNow;
                emp.DeactivateReason = request.DeactivateReason;
            }

            var response = await _employeeRepository.Update(emp);
            var dependents = await _employeeDependentRepository.GetEmployeeDependentByEmpNo(id);
            return response.ToEmployeeResponse(dependents);

        }

        public async Task<object> DeleteEmployee(int id)
        {
            var response = await _employeeRepository.Delete(id);
            var dependents = await _employeeDependentRepository.GetEmployeeDependentByEmpNo(id);
            return response.ToEmployeeResponse(dependents);
        }

        public async Task<object> GetEmployee(int id)
        {
            var emp = await _employeeRepository.GetEmployee(id);
            var employee = await _employeeRepository.GetAllEmployees();
            var dependents = await _employeeDependentRepository.GetEmployeeDependentByEmpNo(id);
            return emp.ToEmployeeDetailResponse(dependents);
        }

        public async Task<List<object>> GetEmployees(int pageNumber, int perPage)
        {
            var employees = await _employeeRepository.GetEmployees(pageNumber, perPage);

            return employees
                .Select(s => s.ToEmployeeResponse(_employeeDependentRepository.GetEmployeeDependentByEmpNo(s.Empno).Result))
                .ToList<object>();
        }

        // NEW ======>
        public async Task<List<object>> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployees();
            return employees.Select(s => s.ToEmployeeResponse(null)).ToList<object>();
                
        }

        public async Task<List<EmployeeSearchResponse>> SearchEmployee(SearchEmployeeQuery query, PageRequest pageRequest)
        {
            var employees = await _employeeRepository.GetAllEmployees();
            
            bool isKeyWord = !string.IsNullOrWhiteSpace(query.KeyWord);
            bool isSearchBy = !string.IsNullOrWhiteSpace(query.SearchBy);
            bool isSort = !string.IsNullOrWhiteSpace(query.SortBy);

            Console.WriteLine(query.KeyWord);
            if (isKeyWord && isSearchBy)
            {
                if(query.SearchBy.Equals("name", StringComparison.OrdinalIgnoreCase))
                {
                    employees = employees
                   .Where(x => x.Fname.ToLower().Contains(query.KeyWord.ToLower())
                   || x.Lname.ToLower().Contains(query.KeyWord.ToLower()));
                };
                if (query.SearchBy.Equals("dept", StringComparison.OrdinalIgnoreCase))
                {
                    employees = employees
                   .Where(x => x.DeptnoNavigation.Deptname.ToLower().Contains(query.KeyWord.ToLower()));
                };
                if (query.SearchBy.Equals("position", StringComparison.OrdinalIgnoreCase))
                {
                    employees = employees
                   .Where(x => x.Position.ToLower().Contains(query.KeyWord.ToLower()));
                };
                if (query.SearchBy.Equals("level", StringComparison.OrdinalIgnoreCase))
                {
                    employees = employees
                   .Where(x => x.EmpLevel == int.Parse(query.KeyWord));
                };
                if (query.SearchBy.Equals("type", StringComparison.OrdinalIgnoreCase))
                {
                    employees = employees
                   .Where(x => x.EmpType.ToLower().Contains(query.KeyWord.ToLower()));
                };
            }

            if (isSort)
            {
                employees = SortEmployeeByField(employees, query.SortBy, query.isDescending);
            }

            if (query.isActive == false)
            {
                employees = employees.Where(w => w.IsActive == false);
            }
            else
            {
                employees = employees.Where(w => w.IsActive == true);
            }

            return await employees
                .Skip((pageRequest.PageNumber - 1) * pageRequest.PerPage)
                .Take(pageRequest.PerPage)
                .Select(s => s.ToEmployeeSearchResponse(s.DeptnoNavigation.Deptname))
                .ToListAsync();
        }

        public async Task<object> UpdateEmployee(int id, UpdateEmployeeRequest request)
        {
            var e = await _employeeRepository.GetEmployee(id);
            if (e == null)
            {
                return null;
            }
            e.Deptno = request.Deptno;
            e.Address = request.Address;
            e.Position = request.Position;
            e.Dob = request.Dob;
            e.Fname = request.Fname;
            e.Lname = request.Lname;
            e.Sex = request.Sex;
            e.UpdatedAt = DateTime.UtcNow;
            e.Ssn = request.Ssn;
            e.Salary = request.Salary;
            e.EmpType = request.EmpType;
            e.EmpLevel = request.EmpLevel;

            //NEW======>
            e.DirectSupervisor = request.DirectSupervisor;
            var response = await _employeeRepository.Update(e);

            // update dependent
            await _employeeDependentRepository.Delete(id);
            foreach (var item in request.EmpDependents)
            {
                var newEmpDependent = new EmployeeDependent()
                {
                    DependentEmpno = id,
                    BirthDate = item.BirthDate,
                    Fname = item.Fname,
                    Lname = item.Lname,
                    Sex = item.Sex,
                    Relation = item.Relation
                };
                await _employeeDependentRepository.Create(newEmpDependent);
            }

            var dependents = await _employeeDependentRepository.GetAllEmployeeDependents();
            return response.ToEmployeeResponse(dependents.Where(w=>w.DependentEmpno == id).ToList());
        }

        MSEmployeeDetailResponse CreateResponse()
        {
            return new MSEmployeeDetailResponse()
            {
                Status = false,
                Message = "",
                Data = null,
            };
        }

        private IQueryable<Employee> SortEmployeeByField(IQueryable<Employee> employees, string field, bool isDescending)
        {
            if(field.Equals("name", StringComparison.OrdinalIgnoreCase))
            {
                employees =  isDescending ? employees.OrderByDescending(x=>x.Fname): employees.OrderBy(x=>x.Fname);
            }
            if (field.Equals("departement", StringComparison.OrdinalIgnoreCase))
            {
                employees = isDescending ? employees.OrderByDescending(x => x.DeptnoNavigation.Deptname) : employees.OrderBy(x => x.DeptnoNavigation.Deptname);
           
            }
            if (field.Equals("position", StringComparison.OrdinalIgnoreCase))
            {
                employees = isDescending ? employees.OrderByDescending(x => x.Position) : employees.OrderBy(x => x.Position);
             
            }
            if (field.Equals("level", StringComparison.OrdinalIgnoreCase))
            {
                employees = isDescending ? employees.OrderByDescending(x => x.EmpLevel) : employees.OrderBy(x => x.EmpLevel);
               
            }
            if (field.Equals("type", StringComparison.OrdinalIgnoreCase))
            {
                employees = isDescending ? employees.OrderByDescending(x => x.EmpType) : employees.OrderBy(x => x.EmpType);
              
            }
            if (field.Equals("updateDate", StringComparison.OrdinalIgnoreCase))
            {
                employees = isDescending ? employees.OrderByDescending(x => x.UpdatedAt) : employees.OrderBy(x => x.UpdatedAt);
              
            }
            return employees;
        }
    }
}

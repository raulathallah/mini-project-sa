using CompanyWeb.Application.Repositories;
using CompanyWeb.Application.Services;
using CompanyWeb.Domain.Models.Dtos;
using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Options;
using CompanyWeb.Domain.Models.Requests;
using CompanyWeb.Domain.Models.Responses;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Infrastructure.Services
{
    public class EmployeeService :  IEmployeeService
    {
        private readonly ICompanyService _companyService;
        private readonly IDepartementRepository _departementRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly CompanyOptions _companyOptions;
        public EmployeeService(ICompanyService companyService, 
            IEmployeeRepository employeeRepository, 
            IDepartementRepository departementRepository,
            IOptions<CompanyOptions> companyOptions)
        {
            _companyService = companyService;
            _employeeRepository = employeeRepository;
            _departementRepository = departementRepository;
            _companyOptions = companyOptions.Value;
        }

        public async Task<object> CreateEmployee(AddEmployeeRequest request)
        {
            var response = CreateResponse();
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
            
            var data = await _employeeRepository.Create(request);
            response.Status = true;
            response.Message = "Success";
            response.Data = data;
            return response;
        }

        public async Task<Employee> DeleteEmployee(int id)
        {
            return await _employeeRepository.Delete(id);
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await _employeeRepository.GetEmployee(id);
        }

        public async Task<List<Employee>> GetEmployees(int pageNumber, int perPage)
        {
            return await _employeeRepository.GetEmployees(pageNumber, perPage);
        }

        public async Task<Employee> UpdateEmployee(int id, UpdateEmployeeRequest request)
        {
            return await _employeeRepository.Update(id, request);
        }

        EmployeeDetailResponse CreateResponse()
        {
            return new EmployeeDetailResponse()
            {
                Status = false,
                Message = "",
                Data = null,
            };
        }
    }
}

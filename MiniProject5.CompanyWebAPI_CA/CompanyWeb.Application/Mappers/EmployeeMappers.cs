using CompanyWeb.Domain.Models.Dtos;
using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Responses.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Application.Mappers
{
    public static class EmployeeMappers
    {
        public static EmployeeResponse ToEmployeeResponse(this Employee model, List<EmployeeDependent> employeeDependents)
        {
            //NEW======>
            return new EmployeeResponse()
            {
                Empno = model.Empno,
                Fname = model.Fname,
                Lname = model.Lname,
                Address = model.Address,
                EmailAddress = model.EmailAddress,
                PhoneNumber = model.PhoneNumber,
                DeactivateReason = model.DeactivateReason,
                DirectSupervisor = model.DirectSupervisor,
                Dob = model.Dob,
                Sex = model.Sex,
                IsActive = model.IsActive,
                Position = model.Position,
                Deptno = model.Deptno,
                Ssn = model.Ssn,
                Salary = model.Salary,
                EmpType = model.EmpType,
                EmpLevel = model.EmpLevel,
                UpdateAt = model.UpdatedAt,
                CreatedAt = model.CreatedAt,
                EmpDependents = employeeDependents,
            };
        }

        public static EmployeeSearchResponse ToEmployeeSearchResponse(this Employee model, string deptName)
        {
            return new EmployeeSearchResponse()
            {
                Name = model.Fname + " " + model.Lname,
                Departement = deptName,
                Position = model.Position,
                EmpLevel = model.EmpLevel,
                EmpType = model.EmpType,
                UpdateAt = model.UpdatedAt
            };
        }

        public static EmployeeDetailResponse ToEmployeeDetailResponse(this Employee model, List<EmployeeDependent> employeeDependents)
        {
            //NEW======>
            return new EmployeeDetailResponse()
            {
                Empno = model.Empno,
                Deptno = model.Deptno,
                DeactivateReason = model.DeactivateReason,
                Dob = model.Dob,
                Sex = model.Sex,
                IsActive = model.IsActive,
                Ssn = model.Ssn,
                Salary = model.Salary,
                Fname = model.Fname,
                Lname = model.Lname,
                Position = model.Position,
                EmpType = model.EmpType,
                EmpLevel = model.EmpLevel,
                Address = model.Address,
                EmailAddress = model.EmailAddress,
                PhoneNumber = model.PhoneNumber,
                DirectSupervisor = model.DirectSupervisor,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,
                EmpDependents = employeeDependents,
            };
        }
    }
}

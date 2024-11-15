using CompanyWeb.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Responses.Employee
{
    public class EmployeeDetailResponse
    {
        //NEW======>
        public string? Fname { get; set; }
        public string? Lname { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }

        // Job Title = Position
        public string? Position { get; set; } = null!;
        public int? DirectSupervisor { get; set; }
        public string? EmpType { get; set; }
        public int? EmpLevel { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<EmployeeDependent>? EmpDependents { get; set; }
        public int Empno { get; set; }
        public DateOnly Dob { get; set; }
        public string DeactivateReason { get; set; }
        public string Sex { get; set; } = null!;
        public string Ssn { get; set; } = null!;
        public int Salary { get; set; }
        public bool IsActive { get; set; }
        public int? Deptno { get; set; }

    }
}

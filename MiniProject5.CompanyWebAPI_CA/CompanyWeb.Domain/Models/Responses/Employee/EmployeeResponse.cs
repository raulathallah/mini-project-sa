using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyWeb.Domain.Models.Entities;

namespace CompanyWeb.Domain.Models.Responses.Employee
{
    public class EmployeeResponse
    {
        public int Empno { get; set; }
        public string Fname { get; set; } = null!;
        public string Lname { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateOnly Dob { get; set; }
        public string PhoneNumber { get; set; }
        public string DeactivateReason { get; set; }
        public string EmailAddress { get; set; } = null!;
        public string Sex { get; set; } = null!;
        public string Position { get; set; } = null!;
        public string Ssn { get; set; } = null!;
        public int Salary { get; set; }
        public bool IsActive { get; set; }
        public int? Deptno { get; set; }
        public string? EmpType { get; set; }
        public int? EmpLevel { get; set; }

        //NEW======>
        public int? DirectSupervisor { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public List<EmployeeDependent>? EmpDependents { get; set; }
    }
}

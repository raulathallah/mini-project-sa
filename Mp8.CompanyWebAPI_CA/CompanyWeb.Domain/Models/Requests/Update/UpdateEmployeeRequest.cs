using CompanyWeb.Domain.Models.Requests.Add;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Requests
{
    public class UpdateEmployeeRequest
    {
        public string Fname { get; set; } = null!;
        public string Lname { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateOnly Dob { get; set; }
        public string Sex { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Ssn { get; set; }
        public int Salary { get; set; }
        public string EmailAddress { get; set; } = null!;
        public string Position { get; set; } = null!;
        public int? Deptno { get; set; }
        public int? DirectSupervisor { get; set; }
        public int EmpLevel { get; set; }
        public string EmpType { get; set; } = null!;
        public List<AddEmployeeDependentRequest>? EmpDependents { get; set; }

    }
}

using CompanyWeb.Domain.Models.Responses.Base;
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
    public class EmployeeSearchResponse
    {
        public int? Empno { get; set; }
        public string? Name { get; set; }
        public string? Departement { get; set; }
        public string? Position { get; set; }
        public int? EmpLevel { get; set; }
        public string? EmpType { get; set; }
        public DateTime? UpdateAt { get; set; }
        public bool IsActive { get; set; }
    }
}

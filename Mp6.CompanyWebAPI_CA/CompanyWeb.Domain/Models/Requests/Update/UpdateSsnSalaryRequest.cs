using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Requests.Update
{
    public class UpdateSsnSalaryRequest
    {
        public string? Ssn { get; set; }
        public int Salary { get; set; }
    }
}

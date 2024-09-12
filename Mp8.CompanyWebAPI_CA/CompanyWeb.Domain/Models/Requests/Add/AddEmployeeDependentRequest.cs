using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Requests.Add
{
    public class AddEmployeeDependentRequest
    {
        public string Fname { get; set; } = null!;
        public string Lname { get; set; } = null!;
        public string Sex { get; set; } = null!;
        public string Relation { get; set; } = null!;
        public DateOnly? BirthDate { get; set; }
    }
}

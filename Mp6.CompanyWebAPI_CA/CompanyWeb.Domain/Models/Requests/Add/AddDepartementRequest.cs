using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Requests.Add
{
    public class AddDepartementRequest
    {
        public string Deptname { get; set; } = null!;
        public int? Mgrempno { get; set; }
        public List<int>? Location { get; set; }
    }
}

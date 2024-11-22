using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Responses
{
    public class DepartementDetailResponse
    {
        public int Deptno { get; set; }
        public string? Deptname { get; set; }
        public int? Mgrempno { get; set; }
        public List<int>? Location { get; set; }
    }
}

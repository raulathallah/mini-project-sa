using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Requests
{
    public class UpdateDepartementRequest
    {
        public string Deptname { get; set; } = null!;
        public int? Mgrempno { get; set; }
        public List<int>? Location { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Requests
{
    public class UpdateProjectRequest
    {
        public string Projname { get; set; } = null!;
        public int Deptno { get; set; }
    }
}

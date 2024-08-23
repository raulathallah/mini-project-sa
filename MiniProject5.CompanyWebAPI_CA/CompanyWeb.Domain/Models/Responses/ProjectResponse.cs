using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Responses
{
    public class ProjectResponse
    {
        public int Projno { get; set; }
        public string? Projname { get; set; }
        public int ProjLocation { get; set; }
        public int Deptno { get; set; }
    }
}

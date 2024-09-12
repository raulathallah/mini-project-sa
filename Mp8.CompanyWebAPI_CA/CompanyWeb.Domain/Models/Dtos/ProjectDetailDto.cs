using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Dtos
{
    public class ProjectDetailDto
    {
        public int Projno { get; set; }
        public string? Projname { get; set; }
        public int Deptno { get; set; }
    }
}

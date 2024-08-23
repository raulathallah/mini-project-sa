using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Dtos
{
    public class WorksOnDetailDto
    {
        public int Empno { get; set; }
        public int Projno { get; set; }
        public DateOnly Dateworked { get; set; }
        public int Hoursworked { get; set; }
    }
}

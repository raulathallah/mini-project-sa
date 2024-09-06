using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Requests.Add
{
    public class AddProjectRequest
    {
        public string Projname { get; set; } = null!;
        public int Deptno { get; set; }
        public int ProjLocation { get; set; }
    }
}

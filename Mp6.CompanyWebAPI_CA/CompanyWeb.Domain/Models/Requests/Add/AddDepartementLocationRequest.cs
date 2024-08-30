using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Requests.Add
{
    public class AddDepartementLocationRequest
    {
        public int Deptno { get; set; }
        public int LocationId { get; set; }
    }
}

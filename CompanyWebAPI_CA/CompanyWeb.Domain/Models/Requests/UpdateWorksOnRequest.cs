using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Requests
{
    public class UpdateWorksOnRequest
    {
        public DateOnly Dateworked { get; set; }
        public int Hoursworked { get; set; }
    }
}

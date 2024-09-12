using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Helpers
{
    public class PageRequest
    {
        public int PageNumber { get; set; }
        public int PerPage { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Helpers
{
    public class PageRequest
    {
        public int PageNumber { get; set; }
        public int PerPage { get; set; }
    }
}

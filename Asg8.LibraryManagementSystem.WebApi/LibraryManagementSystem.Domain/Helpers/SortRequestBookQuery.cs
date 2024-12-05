using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Helpers
{
    public class SortRequestBookQuery
    {
        //sort & size
        public string? SortBy { get; set; } = null;
        public string? SortOrder { get; set; } = "asc";
    }
}

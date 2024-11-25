using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Helpers
{
    public class SearchEmployeeQuery
    {
        public string? KeyWord { get; set; }
        public string? SearchBy { get; set; }
        public string? UpdateDate { get; set; }
        public bool? isActive { get; set; }
        public bool isDescending { get; set; }
        public string? SortBy { get; set; }
    }
}

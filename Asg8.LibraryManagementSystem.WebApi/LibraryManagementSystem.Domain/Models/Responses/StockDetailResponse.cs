using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models.Responses
{
    public class StockDetailResponse
    {
        public int BookId { get; set; }
        public int LocationId { get; set; }
        public int StockCount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models.Requests.Stocks
{
    public class AddStockRequest
    {
        public int BookId { get; set; }
        public int LocationId { get; set; }
        public int Stock { get; set; }
    }
}

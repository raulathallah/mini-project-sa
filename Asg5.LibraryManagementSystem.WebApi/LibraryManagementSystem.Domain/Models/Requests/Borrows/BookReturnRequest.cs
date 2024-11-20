using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models.Requests.Borrows
{
    public class BookReturnRequest
    {
        public List<int>? BookId { get; set; }
        public int UserId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models.Requests.Borrows
{
    public class BookBorrowRequest
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int LocationId { get; set; }
    }
}

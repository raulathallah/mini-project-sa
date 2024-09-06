using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models.Responses
{
    public class BookCheckOutLibrarianBookResponse
    {
        public string? Isbn { get; set; }
        public string? Title { get; set; }
        public DateOnly BorrowedAt { get; set; }
        public DateOnly DueDate { get; set; }
        public int Location { get; set; }
    }
}

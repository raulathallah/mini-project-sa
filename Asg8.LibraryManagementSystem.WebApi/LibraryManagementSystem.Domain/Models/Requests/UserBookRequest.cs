using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models.Requests
{
    public class UserBookRequest
    {
        public string? Title { get; set; }
        public string? Isbn { get; set; }
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public int? LocationId { get; set; }
    }
}

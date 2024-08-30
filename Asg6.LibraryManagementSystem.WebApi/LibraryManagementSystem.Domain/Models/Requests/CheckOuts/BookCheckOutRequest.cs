using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models.Requests.CheckOuts
{
    public class BookCheckOutRequest
    {
        public string? Isbn { get; set; }
        public int? UserId { get; set; }
        public string AppUserId { get; set; } = null!;
        public int? LocationId { get; set; }
    }
}

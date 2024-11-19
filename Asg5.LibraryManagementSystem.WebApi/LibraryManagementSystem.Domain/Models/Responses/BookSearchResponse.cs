using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models.Responses
{
    public class BookSearchResponse
    {
        public string Title { get; set; } = null!;
        public int BookId { get; set; }
        public string Category { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string Publisher { get; set; } = null!;
        public string Isbn { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string[]? Locations { get; set; } = null!;
    }
}

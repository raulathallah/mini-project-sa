using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Core.Models.Responses
{
    public class BookDetailResponse
    {
        public int BookId { get; set; }
        public string Title { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string Publisher { get; set; } = null!;
        public string Isbn { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string DeleteReason { get; set; } = null!;
        public int Price { get; set; }
        public int Stock { get; set; }
        public bool IsDeleted { get; set; }
        public string Language { get; set; } = null!;
    }
}

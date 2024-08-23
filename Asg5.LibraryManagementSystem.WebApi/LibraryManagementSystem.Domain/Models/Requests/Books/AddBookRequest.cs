using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models.Requests.Books
{
    public class AddBookRequest
    {
        public string Title { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Publisher { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string Isbn { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Language { get; set; } = null!;
        public int Price { get; set; }
    }
}

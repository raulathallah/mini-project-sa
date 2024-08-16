using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.Models.Responses
{
    public class BookDetailResponse
    {
        public int Bookid { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public int Publicationyear { get; set; }
        public string? Isbn { get; set; }
    }
}

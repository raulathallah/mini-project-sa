using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models.Entities
{
    [Table("bookrequests")]
    public class BookRequest
    {
        [Key]
        [Column("bookrequestid")]
        public int BookRequestId { get; set; }

        [Column("title")]
        public string? Title { get; set; }

        [Column("isbn")]
        public string? Isbn { get; set; }

        [Column("author")]
        public string? Author { get; set; }

        [Column("publisher")]
        public string? Publisher { get; set; }

        [Column("startdate")]
        public DateOnly? StartDate { get; set; }

        [Column("enddate")]
        public DateOnly? EndDate { get; set; }

        [Column("processid")]
        public int ProcessId { get; set; }
    }
}

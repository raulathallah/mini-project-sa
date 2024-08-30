using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models.Responses
{
    public class BookCheckOutResponse
    {
        public string? Isbn { get; set; }
        public string? Title { get; set; }
        public DateOnly DueDate {  get; set; }
    }
}

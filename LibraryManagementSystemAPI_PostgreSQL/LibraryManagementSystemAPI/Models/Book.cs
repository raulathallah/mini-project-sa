using LibraryManagementSystemAPI.Dto;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystemAPI.Models
{
    public class Book 
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Author { get; set; }
        [Range(0, int.MaxValue)]
        public int PublicationYear { get; set; }
        [StringLength(13)]
        public string Isbn { get; set; }
    }


}

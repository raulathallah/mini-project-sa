using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystemAPI.Dto
{
    public class BookAddDto
    {
        [Required]
        public string Title { get; set; }
        public string Author { get; set; }
        [Range(0, int.MaxValue)]
        public int PublicationYear { get; set; }
        [StringLength(13)]
        public string Isbn { get; set; }
    }
}

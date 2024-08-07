using LibraryManagementSystemAPI.Models;

namespace LibraryManagementSystemAPI.Dto
{
    public class BookDetailDto : BookResponseDto
    {
        public Book Data { get; set; }
    }
}

using LibraryManagementSystemAPI.Models;

namespace LibraryManagementSystemAPI.Dto
{
    public class BookListDto : BookResponseDto
    {
        public List<Book> Data { get; set; }
    }
}

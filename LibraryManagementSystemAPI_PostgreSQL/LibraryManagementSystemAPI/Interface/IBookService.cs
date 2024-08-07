using LibraryManagementSystemAPI.Dto;
using LibraryManagementSystemAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemAPI.Interface
{
    public interface IBookService
    {
        Task<BookListDto> GetAllBook();
        Task<BookDetailDto> GetBookById(int id);
        Task<BookDetailDto> AddNewBook(BookAddDto book);
        Task<BookDetailDto> UpdateBook(int id, BookAddDto book);
        Task<BookDetailDto> DeleteBook(int id);

    }
}

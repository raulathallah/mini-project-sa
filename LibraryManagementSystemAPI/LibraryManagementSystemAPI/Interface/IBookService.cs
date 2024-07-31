using LibraryManagementSystemAPI.Dto;
using LibraryManagementSystemAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemAPI.Interface
{
    public interface IBookService
    {
        BookListDto GetAllBook();
        BookDetailDto GetBookById(int id);
        BookDetailDto AddNewBook(BookAddDto book);
        BookDetailDto UpdateBook(int id, BookAddDto book);
        BookDetailDto DeleteBook(int id);

    }
}

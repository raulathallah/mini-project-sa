using LMS.Core.Models;
using LMS.Core.Models.Requests;
using LMS.Domain.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.IRepositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBook();
        Task<Book> GetBookById(int bookId);
        Task<Book> AddNewBook(AddBookRequest request);
        Task<Book> UpdateBook(int bookId, UpdateBookRequest request);
        Task<Book> DeleteBook(int bookId);
    }
}

using LibraryManagementSystem.Application.Helpers;
using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.Domain.Helpers;
using LibraryManagementSystem.Domain.Models.Requests.Books;
using LibraryManagementSystem.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Service
{
    public interface IBookService
    {
        Task<IEnumerable<object>> GetAllBook();
        Task<Book> GetBookById(int bookId);
        Task<Book> AddNewBook(AddBookRequest request);
        Task<Book> UpdateBook(int bookId, UpdateBookRequest request);
        Task<Book> DeleteBook(int bookId, DeleteBookRequest request);


        // Search //
        Task<object> GetAllBookSearchPaged(SearchBookQuery query, PageRequest pageRequest);

        // Generate Report //
        Task<byte[]> GenerateOverdueReportPDF();
        Task<byte[]> GenerateSignOutBookReportPDF(DateOnly startDate, DateOnly endDate);
        Task<byte[]> GenerateBookPurchaseReportPDF(DateOnly startDate, DateOnly endDate);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.Domain.Models.Requests;

namespace LibraryManagementSystem.Domain.Repositories
{
    public interface IBookRepository
    {
        Task<IQueryable<Book>> GetAll();
        Task<Book> Get(int bookId);
        Task<Book> Add(Book book);
        Task<Book> Update(Book book);
        Task<Book> Delete(Book book);
    }
}

using LMS.Core.IRepositories;
using LMS.Core.Models;
using LMS.Core.Models.Requests;
using LMS.Domain.Models.Requests;
using LMS.Infrastructure.Factory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly LMSDbContext _context;

        public BookRepository(LMSDbContext context)
        {
            _context = context;
        }

        public async Task<Book> AddNewBook(AddBookRequest request)
        {

            if(_context.Books.Any(b=>b.Title == request.Title))
            {
                return null;
            }

            Book newBook = BookFactory.CreateBook(request);
            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();
            return newBook;
        }

        public async Task<Book> DeleteBook(int bookId)
        {
            var deleteBook = await _context.Books.Where(b => b.Bookid == bookId).FirstOrDefaultAsync();
            if (deleteBook == null)
            {
                return null;
            }
            _context.Books.Remove(deleteBook);
            await _context.SaveChangesAsync();
            return deleteBook;
        }

        public async Task<List<Book>> GetAllBook()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetBookById(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            return book;
        }

        public async Task<Book> UpdateBook(int bookId, UpdateBookRequest request)
        {
            var updateBook = await _context.Books.Where(b => b.Bookid == bookId).FirstOrDefaultAsync();

            if (updateBook == null)
            {
                return null;
            }

            updateBook.Title = request.Title;
            updateBook.Author = request.Author;
            updateBook.Publicationyear = request.PublicationYear;
            updateBook.Isbn = request.Isbn;

            _context.Books.Update(updateBook);
            await _context.SaveChangesAsync();
            return updateBook;
        }
    }
}

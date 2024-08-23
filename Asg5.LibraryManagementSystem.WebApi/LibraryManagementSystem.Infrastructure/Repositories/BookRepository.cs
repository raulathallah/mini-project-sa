
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Domain.Repositories;
using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.Domain.Models.Requests;
using LibraryManagementSystem.Infrastructure.Context;

namespace LibraryManagementSystem.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LMSDbContext Context;

        public BookRepository(LMSDbContext context)
        {
            Context = context;
        }

        public async Task<Book> Add(Book book)
        {
            Context.Books.Add(book);
            await Context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> Delete(Book book)
        {
            Context.Books.Update(book);
            await Context.SaveChangesAsync();
            return book;    
        }

        public async Task<IQueryable<Book>> GetAll()
        {
            return Context.Books;
        }

        public async Task<Book> Get(int bookId)
        {
            return await Context.Books.FindAsync(bookId);
        }

        public async Task<Book> Update(Book book)
        {
            Context.Books.Update(book);
            await Context.SaveChangesAsync();
            return book;
        }
    }
}

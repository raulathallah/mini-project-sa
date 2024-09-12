using LibraryManagementSystem.Domain.Models.Entities;
using LibraryManagementSystem.Domain.Repositories;
using LibraryManagementSystem.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Infrastructure.Repositories
{
    public class BookUserTransactionRepository : IBookUserTransactionRepository
    {
        private readonly LMSDbContext Context;
        public BookUserTransactionRepository(LMSDbContext context)
        {
            Context = context;
        }
        public async Task<BookUserTransactions> Add(BookUserTransactions bookUserTransactions)
        {
            Context.BookUserTransactions.Add(bookUserTransactions);
            await Context.SaveChangesAsync();
            return bookUserTransactions;
        }

        public async Task<BookUserTransactions> Delete(BookUserTransactions bookUserTransactions)
        {
            Context.BookUserTransactions.Remove(bookUserTransactions);
            await Context.SaveChangesAsync();
            return bookUserTransactions;
        }

        public async Task<BookUserTransactions> Get(int bookUserTransactionId)
        {
            return await Context.BookUserTransactions.FindAsync(bookUserTransactionId);
        }

        public async Task<IQueryable<BookUserTransactions>> GetAll()
        {
            return Context.BookUserTransactions;
        }

        public async Task<BookUserTransactions> Update(BookUserTransactions bookUserTransactions)
        {
            Context.BookUserTransactions.Update(bookUserTransactions);
            await Context.SaveChangesAsync();
            return bookUserTransactions;
        }
    }
}

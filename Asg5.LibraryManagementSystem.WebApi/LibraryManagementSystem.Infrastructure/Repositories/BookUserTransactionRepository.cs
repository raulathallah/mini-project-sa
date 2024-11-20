using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.Domain.Models.Entities;
using LibraryManagementSystem.Domain.Models.Requests.Transactions;
using LibraryManagementSystem.Domain.Repositories;
using LibraryManagementSystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<BookUserTransactions>> GetByUserId(int userId)
        {
            return await Context.BookUserTransactions.Where(w => w.UserId == userId).ToListAsync();
        }

        public async Task<List<BookUserTransactions>> GetAll()
        {
            return await Context.BookUserTransactions.ToListAsync();
        }

        public async Task<BookUserTransactions> Update(BookUserTransactions bookUserTransactions)
        {
            Context.BookUserTransactions.Update(bookUserTransactions);
            await Context.SaveChangesAsync();
            return bookUserTransactions;
        }

        public async Task<List<BookUserTransactions>> UpdateTransactions(UpdateTransactionRequest request)
        {
            Context.BookUserTransactions.UpdateRange(request.Transactions);
            await Context.SaveChangesAsync();
            return request.Transactions;
        }
    }
}

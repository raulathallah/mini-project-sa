using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Repositories
{
    public interface IBookUserTransactionRepository
    {
        Task<IQueryable<BookUserTransactions>> GetAll();
        Task<BookUserTransactions> Get(int bookUserTransactionId);
        Task<BookUserTransactions> Add(BookUserTransactions bookUserTransactions);
        Task<BookUserTransactions> Update(BookUserTransactions bookUserTransactions);
        Task<BookUserTransactions> Delete(BookUserTransactions bookUserTransactions);
    }
}

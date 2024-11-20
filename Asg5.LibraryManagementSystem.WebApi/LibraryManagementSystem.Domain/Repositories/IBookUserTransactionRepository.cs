using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.Domain.Models.Entities;
using LibraryManagementSystem.Domain.Models.Requests.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Repositories
{
    public interface IBookUserTransactionRepository
    {
        Task<List<BookUserTransactions>> GetAll();
        Task<BookUserTransactions> Get(int bookUserTransactionId);
        Task<List<BookUserTransactions>> GetByUserId(int userId);
        Task<BookUserTransactions> Add(BookUserTransactions bookUserTransactions);
        Task<BookUserTransactions> Update(BookUserTransactions bookUserTransactions);
        Task<List<BookUserTransactions>> UpdateTransactions(UpdateTransactionRequest request);
        Task<BookUserTransactions> Delete(BookUserTransactions bookUserTransactions);
    }
}

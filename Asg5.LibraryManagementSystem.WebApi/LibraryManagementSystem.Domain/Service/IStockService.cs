using LibraryManagementSystem.Domain.Models.Entities;
using LibraryManagementSystem.Domain.Models.Requests.Borrows;
using LibraryManagementSystem.Domain.Models.Requests.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Service
{
    public interface IStockService
    {
        Task<object> AddStocks(AddStockRequest request);
        Task<object> BookCheckOut(BookBorrowRequest request);
        Task<object> BookCheckIn(BookReturnRequest request);

        //TRANSACTIONS
        Task<object> GetAllTransactions();
    }
}
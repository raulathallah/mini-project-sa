using LMS.Core.Models.Requests;
using LMS.Core.Models;
using LMS.Domain.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.IRepositories
{
    public interface IBookusertransactionRepository
    {
        Task<List<Bookusertransaction>> GetAllTransactions();
        Task<Bookusertransaction> GetTransactionById(int transactionId);
        Task<List<Bookusertransaction>> AddNewTransaction(AddTransactionRequest request);
        Task<List<Bookusertransaction>> UpdateTransaction(UpdateTransactionRequest request);

        //extra
        Task<int> GetTransactionCountByUser(int userId);
        Task<List<Bookusertransaction>> GetAllTransactionByUser(int userId);
    }
}

using LMS.Application.IRepositories;
using LMS.Core.Models;
using LMS.Domain.Models.Requests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Repository
{
    public class BookusertransactionRepository : IBookusertransactionRepository
    {
        private readonly LMSDbContext _context;
        public BookusertransactionRepository(LMSDbContext context)
        {
            _context = context;
        }

        public async Task<List<Bookusertransaction>> AddNewTransaction(AddTransactionRequest request)
        {
            _context.Bookusertransactions.AddRange(request.Transactions);
            await _context.SaveChangesAsync();
            return request.Transactions;
        }
        public async Task<List<Bookusertransaction>> GetAllTransactionByUser(int userId)
        {
            return await _context.Bookusertransactions.Where(w => w.Userid == userId).ToListAsync();
        }

        public async Task<List<Bookusertransaction>> GetAllTransactions()
        {
            return await _context.Bookusertransactions.ToListAsync();
        }

        public async Task<Bookusertransaction> GetTransactionById(int transactionId)
        {
            return await _context.Bookusertransactions.Where(w => w.Transactionid == transactionId).FirstOrDefaultAsync();
        }

        public async Task<int> GetTransactionCountByUser(int userId)
        {
            return await _context.Bookusertransactions.Where(bu => bu.Userid == userId && bu.Isreturned == false).CountAsync();
        }

        public async Task<List<Bookusertransaction>> UpdateTransaction(UpdateTransactionRequest request)
        {
            _context.Bookusertransactions.UpdateRange(request.Transactions);
            await _context.SaveChangesAsync();
            return request.Transactions;
        }
    }
}

using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Repositories
{
    public interface IStockRepository
    {
        Task<Stock> Add(Stock stock);
        Task<Stock> Update(Stock stock);
        Task<Stock> Delete(Stock stock);
        Task<Stock> Get(int bookId, int locationId);
        
        Task<IQueryable<Stock>> GetAll();
    }
}

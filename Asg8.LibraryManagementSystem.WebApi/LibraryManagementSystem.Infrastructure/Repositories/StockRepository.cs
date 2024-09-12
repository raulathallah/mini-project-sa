using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.Domain.Models.Entities;
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
    public class StockRepository : IStockRepository
    {
        private readonly LMSDbContext Context;

        public StockRepository(LMSDbContext context)
        {
            Context = context;
        }
        public async Task<Stock> Add(Stock stock)
        {
            Context.Stocks.Add(stock);
            await Context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock> Delete(Stock stock)
        {
            Context.Stocks.Update(stock);
            await Context.SaveChangesAsync();
            return stock;
        }

        public async Task<IQueryable<Stock>> GetAll()
        {
            return Context.Stocks;
        }

        public async Task<Stock> Get(int bookId, int locationId)
        {
            var stock = await Context.Stocks
                .Where(w => w.BookId == bookId && w.LocationId == locationId)
                .FirstOrDefaultAsync();
          
            return stock;
        }

        public async Task<Stock> Update(Stock stock)
        {
            Context.Stocks.Update(stock);
            await Context.SaveChangesAsync();
            return stock;
        }
    }
}

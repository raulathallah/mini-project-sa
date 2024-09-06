using LibraryManagementSystem.Domain.Models.Entities;
using LibraryManagementSystem.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Mappers
{
    public static class StockMappers
    {
        public static StockDetailResponse ToStockResponse(this Stock stock)
        {
            return new StockDetailResponse()
            {
                BookId = stock.BookId,
                LocationId = stock.LocationId,
                StockCount = stock.StockCount,
            };
        }
    }
}

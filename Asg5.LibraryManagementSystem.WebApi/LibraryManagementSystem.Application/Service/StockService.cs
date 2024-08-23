using LibraryManagementSystem.Application.Mappers;
using LibraryManagementSystem.Domain.Models.Entities;
using LibraryManagementSystem.Domain.Models.Requests.Borrows;
using LibraryManagementSystem.Domain.Models.Requests.Stocks;
using LibraryManagementSystem.Domain.Repositories;
using LibraryManagementSystem.Domain.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Service
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IBookUserTransactionRepository _bookUserTransactionRepository;

        public StockService(IStockRepository stockRepository, 
            IBookRepository bookRepository, 
            IBookUserTransactionRepository bookUserTransactionRepository)
        {
            _stockRepository = stockRepository;
            _bookRepository = bookRepository;
            _bookUserTransactionRepository = bookUserTransactionRepository;
        }

        public async Task<object> AddStocks(AddStockRequest request)
        {
            var findStock = await _stockRepository.Get(request.BookId, request.LocationId);
            var findBook = await _bookRepository.Get(request.BookId);
            if (findStock == null)
            {
                var newStock = new Stock()
                {
                    BookId = request.BookId,
                    LocationId = request.LocationId,
                    StockCount = request.Stock,
                };
                var newResponse = await _stockRepository.Add(newStock);

                // update book stock
                findBook.Stock = findBook.Stock + newResponse.StockCount;
                await _bookRepository.Update(findBook);
                return newResponse.ToStockResponse();
            }
            findStock.StockCount = findStock.StockCount + request.Stock;
            var updateResponse = await _stockRepository.Update(findStock);

            // update book stock
            findBook.Stock = findBook.Stock + updateResponse.StockCount;
            await _bookRepository.Update(findBook);
            return updateResponse.ToStockResponse();
        }

        public async Task<object> BookCheckOut(BookBorrowRequest request)
        {
            var findStock = await _stockRepository.Get(request.BookId, request.LocationId);
            var findBook = await _bookRepository.Get(request.BookId);
            if (findStock == null)
            {
                return null;
            }
            // update stock in stocks
            findStock.StockCount = findStock.StockCount - 1;
            await _stockRepository.Update(findStock);

            // update book in books
            findBook.Stock = findBook.Stock - 1;
            await _bookRepository.Update(findBook);

            // add transactions
            var newTransactions = new BookUserTransactions()
            {
                BookId = request.BookId,
                UserId = request.UserId,
                Title = findBook.Title,
                Isbn = findBook.Isbn,
                LocationId = request.LocationId,
                DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(7))
            };
            var response = await _bookUserTransactionRepository.Add(newTransactions);
            return response.ToCheckOutResponse();

        }

    }
}

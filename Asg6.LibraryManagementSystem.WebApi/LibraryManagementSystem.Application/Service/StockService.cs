using LibraryManagementSystem.Application.Mappers;
using LibraryManagementSystem.Domain.Models.Entities;
using LibraryManagementSystem.Domain.Models.Requests.CheckOuts;
using LibraryManagementSystem.Domain.Models.Requests.Stocks;
using LibraryManagementSystem.Domain.Repositories;
using LibraryManagementSystem.Domain.Service;
using Microsoft.AspNetCore.Identity;
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
        private readonly IUserRepository _userRepository;
        private readonly IBookUserTransactionRepository _bookUserTransactionRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public StockService(IStockRepository stockRepository, 
            IBookRepository bookRepository, 
            IUserRepository userRepository,
            IBookUserTransactionRepository bookUserTransactionRepository, 
            UserManager<AppUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _stockRepository = stockRepository;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _bookUserTransactionRepository = bookUserTransactionRepository;
            _userManager = userManager;
            _roleManager = roleManager;
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

        public async Task<object> BookCheckOut(BookCheckOutRequest request)
        {
            //find book
            var findBook = await _bookRepository.GetByIsbn(request.Isbn);
            if(findBook == null)
            {
                return null;
            }
           
            //find stock
            var findStock = await _stockRepository.Get(findBook.BookId, request.LocationId.GetValueOrDefault());
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
                BookId = findBook.BookId,
                UserId = request.UserId.GetValueOrDefault(),
                AppUserId = request.AppUserId,
                Title = findBook.Title,
                Isbn = findBook.Isbn,
                LocationId = request.LocationId.GetValueOrDefault(),
                DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(7)),
                BorrowedAt = DateOnly.FromDateTime(DateTime.Now)
            };
            var response = await _bookUserTransactionRepository.Add(newTransactions);
            return response.ToCheckOutResponse();
        }

        public async Task<object> BookCheckOutUserInformation(BookCheckOutUserRequest request)
        {
           // var user = await _userRepository.GetByAppUserId(request.AppUserId);
            var user = await _userRepository.GetByLibraryCardNumber(request.LibraryCardNumber);
            var transactions = await _bookUserTransactionRepository.GetAll();
            if(user == null)
            {
                return null;
            }
            return user.ToLibrarianCheckOutUserResponse(transactions.Where(w=>w.UserId == user.UserId).Count());
        }
         
        public async Task<object> BookCheckOutBookInformation(BookCheckOutBookRequest request)
        {
            var transactions = await _bookUserTransactionRepository.GetAll();
            var findTransactions = transactions.Where(w => w.Isbn == request.Isbn);
            return findTransactions.Select(s => s.ToLibrarianCheckOutBookResponse());
        }
    }
}

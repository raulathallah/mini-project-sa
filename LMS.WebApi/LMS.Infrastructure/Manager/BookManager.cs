using LMS.Application.IRepositories;
using LMS.Core.IRepositories;
using LMS.Core.Models;
using LMS.Core.Models.Requests;
using LMS.Core.Services;
using LMS.Domain.Models.Requests;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Manager
{
    public class BookManager : IBookManager
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBookusertransactionRepository _bookusertransactionRepository;
        private readonly LibraryOptions _libraryOptions;

        public BookManager(IBookRepository bookRepository, IUserRepository userRepository, IBookusertransactionRepository bookusertransactionRepository, IOptions<LibraryOptions> libraryOptions)
        {
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _bookusertransactionRepository = bookusertransactionRepository;
            _libraryOptions = libraryOptions.Value;
        }

        public async Task<object> BorrowBook(BorrowBookRequest request)
        {
            var borrowed = await _bookusertransactionRepository.GetTransactionCountByUser(request.UserId);
            if (request.BookId.Count() + borrowed > _libraryOptions.MaxBookBorrowed)
            {
                return $"You already borrowed {borrowed} book(s). ({borrowed}/3)";
            }

            if (request.BookId.Count() > _libraryOptions.MaxBookBorrowed)
            {
                return $"You can only borrow 3 books at once! Please try again. ({borrowed}/3)";
            }

            List<Bookusertransaction> transactions = new();
            foreach (var item in request.BookId)
            {
                transactions.Add(new Bookusertransaction()
                {
                    Bookid = item,
                    Userid = request.UserId,
                    Borrowdate = DateOnly.FromDateTime(DateTime.Now),
                    Borrowexpired = DateOnly.FromDateTime(DateTime.Now.AddDays(_libraryOptions.MaxBookLoanDuration)),
                    Isreturned = false,
                    Returndate = DateOnly.FromDateTime(DateTime.Now.AddDays(_libraryOptions.MaxBookLoanDuration))
                });
            }

            var response = await _bookusertransactionRepository.AddNewTransaction(new AddTransactionRequest() { Transactions = transactions});
            return response;
        }

        public async Task<object> ReturnBook(ReturnBookRequest request)
        {
            var returnBook = await _bookusertransactionRepository.GetAllTransactionByUser(request.UserId);

            if (returnBook == null)
            {
                return $"No transaction available for user with ID {request.UserId}";
            }

            foreach (var item in returnBook)
            {

                if (request.BookId.Any(a => a.Equals(item.Bookid)))
                {
                    if (item.Isreturned == true)
                    {
                        return $"Book with ID {item.Bookid} already returned! Please try again.";
                    }
                    item.Returndate = DateOnly.FromDateTime(DateTime.Now);
                    item.Isreturned = true;
                }
            }

            var response = await _bookusertransactionRepository.UpdateTransaction(new UpdateTransactionRequest() { Transactions = returnBook });
            return response;
        }
    }
}

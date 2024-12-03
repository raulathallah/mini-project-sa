using LibraryManagementSystem.Application.Helpers;
using LibraryManagementSystem.Application.Mappers;
using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.Core.Models.Responses;
using LibraryManagementSystem.Domain.Helpers;
using LibraryManagementSystem.Domain.Models.Requests.Books;
using LibraryManagementSystem.Domain.Models.Responses;
using LibraryManagementSystem.Domain.Repositories;
using LibraryManagementSystem.Domain.Service;
using LMS.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TheArtOfDev.HtmlRenderer.Core;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace LibraryManagementSystem.Application.Service
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookUserTransactionRepository _bookUserTransactionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IStockService _stockService;
        private readonly LibraryOptions _libraryOptions;
        private readonly IPdfService _pdfService;


        public BookService(IBookRepository bookRepository, 
            IStockService stockService,
            IBookUserTransactionRepository bookUserTransactionRepository,
            IUserRepository userRepository,
            IOptions<LibraryOptions> libraryOptions,
            IPdfService pdfService)
        {
            _bookRepository = bookRepository;
            _stockService = stockService;
            _bookUserTransactionRepository = bookUserTransactionRepository;
            _userRepository = userRepository;
            _libraryOptions = libraryOptions.Value;
            _pdfService = pdfService;
        }

        public async Task<Book> AddNewBook(AddBookRequest request)
        {
            var newBook = new Book()
            {
                Category = request.Category,
                Description = request.Description,
                Isbn = request.Isbn,
                Price = request.Price,
                Stock = 0,
                Publisher = request.Publisher,
                Author = request.Author,
                Language = request.Language,
                Title = request.Title,
                DeleteReason = "",
                IsDeleted = false
            };
            var book = await _bookRepository.Add(newBook);
            return book;
        }

        public async Task<Book> DeleteBook(int bookId, DeleteBookRequest request)
        {
            var book = await _bookRepository.Get(bookId);
            book.DeleteReason = request.DeleteReason;
            book.IsDeleted = true;

            var deleteBook = await _bookRepository.Delete(book);
            if(deleteBook == null)
            {
                return null;
            }
            return deleteBook;
        }

        public async Task<IEnumerable<object>> GetAllBook()
        {
            var books = await _bookRepository.GetAll();
            return books.OrderBy(ob=>ob.BookId).Select(book => book.ToBookResponse());
        }

        public async Task<Book> GetBookById(int bookId)
        {
            var book = await _bookRepository.Get(bookId);
            return book;
        }

        public async Task<Book> UpdateBook(int bookId, UpdateBookRequest request)
        {
            var book = await _bookRepository.Get(bookId);
            book.Title = request.Title;
            book.Price = request.Price;
            book.Description = request.Description;
            book.Author = request.Author;
            book.Category = request.Category;
            book.Publisher = request.Publisher;
            book.Isbn = request.Isbn;

            var updateBook = await _bookRepository.Update(book);
            if(updateBook == null)
            {
                return null;
            }
            return updateBook;
        }

        public async Task<object> GetAllBookSearchPaged(SearchBookQuery query, PageRequest pageRequest)
        {
            var books = await _bookRepository.GetAll();

            bool isTitle = !string.IsNullOrWhiteSpace(query.Title);
            bool isAuthor = !string.IsNullOrWhiteSpace(query.Author);
            bool isCategory = !string.IsNullOrWhiteSpace(query.Category);
            bool isIsbn = !string.IsNullOrWhiteSpace(query.Isbn);
            bool isLanguage = !string.IsNullOrWhiteSpace(query.Language);


            if (isLanguage)
            {
                books = books.Where(w => w.Language.ToLower() == query.Language.ToLower());
            }

            // Only Title
            if (isTitle && !isAuthor && !isCategory && !isIsbn)
            {
                books = books
                        .Where(w =>
                            w.Title
                            .ToLower()
                            .Contains(query.Title.ToLower())
                        );
            }

            // Only Author
            if (!isTitle && isAuthor && !isCategory && !isIsbn)
            {
                books = books
                        .Where(w =>
                            w.Author
                            .ToLower()
                            .Contains(query.Author.ToLower())
                        );
            }

            // Only Category
            if (!isTitle && !isAuthor && isCategory && !isIsbn)
            {
                books = books
                        .Where(w =>
                            w.Category
                            .ToLower()
                            .Contains(query.Category.ToLower())
                        );
            }

            // Only Isbn
            if (!isTitle && !isAuthor && !isCategory && isIsbn)
            {
                books = books
                        .Where(w =>
                            w.Isbn
                            .ToLower()
                            .Contains(query.Isbn.ToLower())
                        );
            }


            // Author + Category
            if (isAuthor && isCategory && !isTitle && !isIsbn)
            {
                if (query.AndOr2 == "or")
                {
                    books = books.Where(w =>
                    w.Author.ToLower().Contains(query.Author.ToLower())
                    ||
                    w.Category.ToLower().Contains(query.Category.ToLower())
                    );
                }
                else
                {
                    books = books.Where(w =>
                    w.Author.ToLower().Contains(query.Author.ToLower())
                    &&
                    w.Category.ToLower().Contains(query.Category.ToLower())
                    );
                }
            }

            // Category + Isbn
            if (isCategory && isIsbn)
            {
                if (query.AndOr3 == "or")
                {
                    books = books.Where(w =>
                    w.Category.ToLower().Contains(query.Category.ToLower())
                    ||
                    w.Isbn.ToLower().Contains(query.Isbn.ToLower())
                    );
                }
                else
                {
                    books = books.Where(w =>
                    w.Category.ToLower().Contains(query.Category.ToLower())
                    &&
                    w.Isbn.ToLower().Contains(query.Isbn.ToLower())
                    );
                }
            }

            // Title -> Author -> Category -> Isbn //
            if (isTitle && isAuthor)
            {
                if (query.AndOr1 == "or")
                {
                    if (isTitle && isCategory && isAuthor)
                    {
                        // __ or __ or __    
                        if (query.AndOr2 == "or")
                        {
                            if (isTitle && isCategory && isAuthor && isIsbn)
                            {
                                // __ or __ or __ or
                                if (query.AndOr3 == "or")
                                {
                                    books = books
                                        .Where(w =>
                                            w.Title
                                            .ToLower()
                                            .Contains(query.Title.ToLower())
                                            ||
                                            w.Category
                                            .ToLower()
                                            .Contains(query.Category.ToLower())
                                            ||
                                            w.Author
                                            .ToLower()
                                            .Contains(query.Author.ToLower())
                                            ||
                                            w.Isbn
                                            .ToLower()
                                            .Contains(query.Isbn.ToLower())
                                        );
                                }
                                // __ or __ or __ and
                                else
                                {
                                    books = books
                                        .Where(w =>
                                            w.Title
                                            .ToLower()
                                            .Contains(query.Title.ToLower())
                                            ||
                                            w.Category
                                            .ToLower()
                                            .Contains(query.Category.ToLower())
                                            ||
                                            w.Author
                                            .ToLower()
                                            .Contains(query.Author.ToLower())
                                            &&
                                            w.Isbn
                                            .ToLower()
                                            .Contains(query.Isbn.ToLower())
                                        );
                                }
                            }
                            else
                            {
                                books = books
                                    .Where(w =>
                                        w.Title
                                        .ToLower()
                                        .Contains(query.Title.ToLower())
                                        ||
                                        w.Category
                                        .ToLower()
                                        .Contains(query.Category.ToLower())
                                        ||
                                        w.Author
                                        .ToLower()
                                        .Contains(query.Author.ToLower())
                                    );
                            }


                        }
                        // __ or __ and __
                        else
                        {
                            books = books
                                .Where(w =>
                                    w.Title
                                    .ToLower()
                                    .Contains(query.Title.ToLower())
                                    ||
                                    w.Category
                                    .ToLower()
                                    .Contains(query.Category.ToLower())
                                    &&
                                    w.Author
                                    .ToLower()
                                    .Contains(query.Author.ToLower())

                                );
                        }
                    }
                    else
                    {
                        books = books
                               .Where(w =>
                                   w.Title
                                   .ToLower()
                                   .Contains(query.Title.ToLower())
                                   ||
                                   w.Author
                                   .ToLower()
                                   .Contains(query.Author.ToLower())

                               );
                    }
                }
                // __ and __
                else
                {
                    if (isTitle && isAuthor && isCategory)
                    {
                        // __ and __ or __    
                        if (query.AndOr2 == "or")
                        {
                            if (isTitle && isAuthor && isCategory && isIsbn)
                            {
                                // __ and __ or __ or
                                if (query.AndOr3 == "or")
                                {
                                    books = books
                                        .Where(w =>
                                            w.Title
                                            .ToLower()
                                            .Contains(query.Title.ToLower())
                                            &&
                                            w.Author
                                            .ToLower()
                                            .Contains(query.Author.ToLower())
                                            ||
                                            w.Category
                                            .ToLower()
                                            .Contains(query.Category.ToLower())
                                            ||
                                            w.Isbn
                                            .ToLower()
                                            .Contains(query.Isbn.ToLower())
                                        );
                                }
                                // __ and __ or __ and
                                else
                                {
                                    books = books
                                        .Where(w =>
                                            w.Title
                                            .ToLower()
                                            .Contains(query.Title.ToLower())
                                            &&
                                            w.Author
                                            .ToLower()
                                            .Contains(query.Author.ToLower())
                                            ||
                                            w.Category
                                            .ToLower()
                                            .Contains(query.Category.ToLower())
                                            &&
                                            w.Isbn
                                            .ToLower()
                                            .Contains(query.Isbn.ToLower())
                                        );
                                }
                            }
                            else
                            {
                                books = books
                                    .Where(w =>
                                        w.Title
                                        .ToLower()
                                        .Contains(query.Title.ToLower())
                                        &&
                                        w.Author
                                        .ToLower()
                                        .Contains(query.Author.ToLower())
                                        ||
                                        w.Category
                                        .ToLower()
                                        .Contains(query.Category.ToLower())
                                    );
                            }


                        }
                        // __ and __ and __
                        else
                        {
                            if (isTitle && isCategory && isAuthor && isIsbn)
                            {
                                // __ and __ and __ or
                                if (query.AndOr3 == "or")
                                {
                                    books = books
                                        .Where(w =>
                                            w.Title
                                            .ToLower()
                                            .Contains(query.Title.ToLower())
                                            &&
                                            w.Author
                                            .ToLower()
                                            .Contains(query.Author.ToLower())
                                            &&
                                            w.Category
                                            .ToLower()
                                            .Contains(query.Category.ToLower())
                                            ||
                                            w.Isbn
                                            .ToLower()
                                            .Contains(query.Isbn.ToLower())
                                        );
                                }
                                // __ and __ and __ and
                                else
                                {
                                    books = books
                                        .Where(w =>
                                            w.Title
                                            .ToLower()
                                            .Contains(query.Title.ToLower())
                                            &&
                                            w.Author
                                            .ToLower()
                                            .Contains(query.Author.ToLower())
                                            &&
                                            w.Category
                                            .ToLower()
                                            .Contains(query.Category.ToLower())
                                            &&
                                            w.Isbn
                                            .ToLower()
                                            .Contains(query.Isbn.ToLower())
                                        );
                                }
                            }
                            else
                            {
                                books = books
                                   .Where(w =>
                                       w.Title
                                       .ToLower()
                                       .Contains(query.Title.ToLower())
                                       &&
                                       w.Author
                                       .ToLower()
                                       .Contains(query.Author.ToLower())
                                       &&
                                       w.Category
                                       .ToLower()
                                       .Contains(query.Category.ToLower())

                                   );
                            }

                        }
                    }
                    else
                    {
                        books = books
                                    .Where(w =>
                                        w.Title
                                        .ToLower()
                                        .Contains(query.Title.ToLower())
                                        &&
                                        w.Author
                                        .ToLower()
                                        .Contains(query.Author.ToLower())
                                    );
                    }

                }
            }

            var total = books.Count();

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                switch (query.SortBy)
                {

                    case "category":

                        books = query.SortOrder.Equals("asc") ? books.OrderBy(s => s.Category) :
                        books.OrderByDescending(s => s.Category);
                        break;

                    case "author":

                        books = query.SortOrder.Equals("asc") ? books.OrderBy(s => s.Author) :
                        books.OrderByDescending(s => s.Author);
                        break;

                    case "isbn":

                        books = query.SortOrder.Equals("asc") ? books.OrderBy(s => s.Isbn) :
                        books.OrderByDescending(s => s.Isbn);
                        break;

                    default:

                        books = query.SortOrder.Equals("asc") ? books.OrderBy(s => s.Title) :
                        books.OrderByDescending(s => s.Title);
                        break;
                }
            }


            var list = books
                //.OrderBy(ob => ob.Title)
                .Skip((pageRequest.PageNumber - 1) * pageRequest.PerPage)
                .Take(pageRequest.PerPage)
                .Select(s => s.ToBookSearchResponse(s.Stocks.Select(sm => sm.LocationIdNavigation.LocationName).ToArray()))
                //.Select(s => s.ToBookResponse())
                .ToList();



            return new
            {
                Total = total,
                Page = pageRequest.PageNumber,
                Data = list
            };

        }

        //SRS-035
        public async Task<byte[]> GenerateOverdueReportPDF()
        {
            string htmlcontent = String.Empty;

            var bookList = await _bookRepository.GetAll();
            var userList = await _userRepository.GetAll();
            var bookTransaction = await _bookUserTransactionRepository.GetAll();
            var bookOverdue = bookTransaction.Where(w => w.DueDate < DateOnly.FromDateTime(DateTime.Now)).ToList();
/*
            var ma = bookTransaction
                .GroupBy(gb=>gb.UserId)
                .Select(s=>new
                {
                    UserId = s.Key,
                    Count = s.Count(),
                })
                .OrderByDescending(ob=>ob.Count)
                .ToList();*/
/*            //
            htmlcontent += "<h1>Library KPIs</h1>";

            //total books
            htmlcontent += "<h3>Total Books: <b>" + bookList.Count() +"</b></h3>";

            //most active members
            htmlcontent += "<h3>Most Active Members</h3>";
            htmlcontent += "<table>";
            htmlcontent +=
                "<tr>" +
                "<td>User ID</td>" +
                "<td>Name</td>" +
                "<td>Position</td>" +
                "<td>Library Card Number</td>" +
                "<td>Transaction Recorded</td>" +
                "</tr>";
            foreach (var value in ma)
            {
                var user = await _userRepository.Get(value.UserId);

                htmlcontent += "<tr>";
                htmlcontent += "<td>" + value.UserId + "</td>";
                htmlcontent += "<td>" + user.FName + " " + user.LName + "</td>";
                htmlcontent += "<td>" + user.UserPosition + "</td>";
                htmlcontent += "<td>" + user.LibraryCardNumber + "</td>";
                htmlcontent += "<td>" + value.Count + "</td>";
                htmlcontent += "</tr>";
            }
            htmlcontent += "</table>";*/

            //overdue books
            htmlcontent += "<h3>Overdue Books</h3>";
            htmlcontent += "<table>";
            htmlcontent +=
                "<tr>" +
                "<td>User</td>" +
                "<td>Book</td>" +
                "<td>Days Overdue</td>" +
                "<td>Penalty</td>" +
                "</tr>";
            foreach (var value in bookOverdue)
            {
                var user = await _userRepository.Get(value.UserId);
                var overdue = DateOnly.FromDateTime(DateTime.Now).Day - value.DueDate.Day;
                var penalty = overdue * _libraryOptions.PenaltyPerDay;

                htmlcontent += "<tr>";
                htmlcontent += "<td>" + user.FName + " " + user.LName + "</td>";
                htmlcontent += "<td>" + value.Title + "</td>";
                htmlcontent += "<td>" + overdue + "</td>";
                htmlcontent += "<td>" + "Rp"+penalty+",00" + "</td>";
                htmlcontent += "</tr>";
            }
            htmlcontent += "</table>";

            return _pdfService.OnGeneratePDF(htmlcontent);
        }

        //SRS-034
        public async Task<byte[]> GenerateSignOutBookReportPDF(DateOnly startDate, DateOnly endDate)
        {
            string htmlcontent = String.Empty;
            var bookTransaction = await _bookUserTransactionRepository.GetAll();
            //var bookList = await _bookRepository.GetAll();
            var allBook = await _bookRepository.GetAll();
            var bookCategories = allBook.Select(s=>s.Category).Distinct().ToList();

            List<BookCategoryCount> bookResult = new();
            foreach (var ctg in bookCategories)
            {
                var c = 0;
                foreach(var book in bookTransaction.Where(w=>w.BorrowedAt >= startDate && w.BorrowedAt <= endDate ).ToList())
                {
                    var b = await _bookRepository.GetByIsbn(book.Isbn);
                    if(b.Category == ctg)
                    {
                        c++;
                    }
                }

                bookResult.Add(new BookCategoryCount()
                {
                    Category = ctg,
                    Count = c
                });
            }

            htmlcontent += "<h3>Sign Out Books by Categories</h3>";
            htmlcontent += "<p>Period: "+ startDate +" to "+ endDate +"</p>";
            htmlcontent += "<table>";
            htmlcontent +=
                "<tr>" +
                "<td>Categories</td>" +
                "<td>Book(s)</td>" +
                "</tr>";
            foreach (var value in bookResult)
            {
                htmlcontent += "<tr>";
                htmlcontent += "<td>"+value.Category+"</td>";
                htmlcontent += "<td>"+value.Count+"</td>";
                htmlcontent += "</tr>";
            }
            htmlcontent += "</table>";

            return _pdfService.OnGeneratePDF(htmlcontent);

        }

        // SRS-037
        public async Task<byte[]> GenerateBookPurchaseReportPDF(DateOnly startDate, DateOnly endDate)
        {
            string htmlcontent = String.Empty;
            var bookTransaction = await _bookUserTransactionRepository.GetAll();

            var bookTransactionDistinct = bookTransaction
                .Where(w => w.BorrowedAt >= startDate && w.BorrowedAt <= endDate)
                .Select(s=>s.Isbn)
                .Distinct()
                .ToList();


            htmlcontent += "<h3>Book purchased</h3>";
            htmlcontent += "<p>Period: " + startDate + " to " + endDate + "</p>";
            htmlcontent += "<table>";
            htmlcontent +=
                "<tr>" +
                "<td>Title</td>" +
                "<td>Category</td>" +
                "<td>Author</td>" +
                "<td>Publisher</td>" +
                "<td>Price</td>" +
                "</tr>";
            foreach (var value in bookTransactionDistinct)
            {
                var book = await _bookRepository.GetByIsbn(value);

                htmlcontent += "<tr>";
                htmlcontent += "<td>" + book.Title + "</td>";
                htmlcontent += "<td>" + book.Category + "</td>";
                htmlcontent += "<td>" + book.Author + "</td>";
                htmlcontent += "<td>" + book.Publisher + "</td>";
                htmlcontent += "<td>" + book.Price + "</td>";
                htmlcontent += "</tr>";
            }
            htmlcontent += "</table>";

            var allBook = await _bookRepository.GetAll();
            var bookCategories = allBook.Select(s => s.Category).Distinct().ToList();
            List<BookCategoryCount> bookResult = new();
            foreach (var ctg in bookCategories)
            {
                var c = 0;
                var totalPaid = 0;
                foreach (var book in bookTransaction.Where(w => w.BorrowedAt >= startDate && w.BorrowedAt <= endDate).ToList())
                {
                    var b = await _bookRepository.GetByIsbn(book.Isbn);
                    if (b.Category == ctg)
                    {
                        c++;
                        totalPaid += b.Price;
                    }
                }

                bookResult.Add(new BookCategoryCount()
                {
                    Category = ctg,
                    TotalMoneyPaid = totalPaid,
                    Count = c
                });
            }

            htmlcontent += "<h3>Total Book Purchased and Money Paid by Category</h3>";
            htmlcontent += "<table>";
            htmlcontent +=
                "<tr>" +
                "<td>Category</td>" +
                "<td>Total Book Purchased</td>" +
                "<td>Total Paid</td>" +
                "</tr>";
            foreach (var value in bookResult)
            {
                htmlcontent += "<tr>";
                htmlcontent += "<td>" + value.Category + "</td>";
                htmlcontent += "<td>" + value.Count + "</td>";
                htmlcontent += "<td>" + value.TotalMoneyPaid + "</td>";
                htmlcontent += "</tr>";
            }
            htmlcontent += "</table>";

            return _pdfService.OnGeneratePDF(htmlcontent);

        }





    }
}

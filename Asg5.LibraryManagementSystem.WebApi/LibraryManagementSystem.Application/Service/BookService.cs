using LibraryManagementSystem.Application.Helpers;
using LibraryManagementSystem.Application.Mappers;
using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.Core.Models.Responses;
using LibraryManagementSystem.Domain.Helpers;
using LibraryManagementSystem.Domain.Models.Requests.Books;
using LibraryManagementSystem.Domain.Models.Responses;
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
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IStockService _stockService;


        public BookService(IBookRepository bookRepository, IStockService stockService)
        {
            _bookRepository = bookRepository;
            _stockService = stockService;
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
            return books.Select(book => book.ToBookResponse());
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
            if(isAuthor && isCategory && !isTitle && !isIsbn)
            {
                if(query.AndOr2 == "or")
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
            if(isCategory && isIsbn)
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
    }

}

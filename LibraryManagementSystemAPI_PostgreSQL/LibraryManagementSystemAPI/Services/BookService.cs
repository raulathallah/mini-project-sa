
using LibraryManagementSystemAPI.Dto;
using LibraryManagementSystemAPI.Interface;
using LibraryManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace LibraryManagementSystemAPI.Services
{
    public class BookService : IBookService
    {
        //private static List<Book> books = new List<Book>();
        private readonly AppDbContext _ctx;

        public BookService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<BookListDto> GetAllBook()
        {
            var books = await _ctx.Books.OrderBy(x=>x.Id).ToListAsync();
            BookListDto response = new BookListDto()
            {
                Data = null,
                Status = false,
                Message = ""
            };

            if(books == null)
            {
                response.Message = "Book list is empty.";
                return response;
            }
         /*   if (books.Count == 0)
            {
                response.Message = "Book list is empty.";
                return response;
            }*/

            response.Data = books;
            response.Status = true;
            response.Message = "Get all book success!";
            return response;
        }

        public async Task<BookDetailDto> GetBookById(int id)
        {
            BookDetailDto response = new BookDetailDto()
            {
                Status = false,
                Message = "",
                Data = null
            };

            //Book res = books.FirstOrDefault(x => x.Id == id);
            //var res = _ctx.Books.Where(b => b.Id == id).FirstOrDefault();
            var res = await _ctx.Books.FindAsync(id);

            if (res == null)
            {
                response.Message = "No book available.";
                return response;
            }

            response.Status = true;
            response.Data = res;
            response.Message = "Get book by ID success!";
            return response;
        }

        public async Task<BookDetailDto> AddNewBook(BookAddDto book)
        {
            BookDetailDto response = new BookDetailDto()
            {
                Status = false,
                Message = "",
                Data = null
            };

            /* if (books.Find(b => b.Title == book.Title) != null)
             {
                 response.Message = "Book already exist.";
                 return response;
             }

             Book result = new Book()
             {
                 Id = books.Count + 1,
                 Title = book.Title,
                 Author = book.Author,
                 PublicationYear = book.PublicationYear,
                 Isbn = book.Isbn,
             };

             //add book to list
             books.Add(result);*/

            
            if (_ctx.Books.Where(b => b.Title == book.Title).FirstOrDefault() != null)
            {
                response.Message = "Book already exist.";
                return response;
            }

            Book result = new Book()
            {
                Title = book.Title,
                Author = book.Author,
                PublicationYear = book.PublicationYear,
                Isbn = book.Isbn,
            };

            _ctx.Books.Add(result);
            await _ctx.SaveChangesAsync();

            //response
            response.Status = true;
            response.Data = result;
            response.Message = "Add book success!";
            return response;
        }
        
        public async Task<BookDetailDto> UpdateBook(int id, BookAddDto book)
        {
            BookDetailDto response = new BookDetailDto()
            {
                Status = false,
                Message = "",
                Data = null
            };

            //Book result = books.Find(b => b.Id == id);
            //var result = _ctx.Books.Where(b=>b.Id == id).FirstOrDefault();
            var result = _ctx.Books.Find(id);
            if (result == null)
            {
                response.Message = "No book available.";
                return response;
            }

            //update value on list
            result.Title = book.Title;
            result.Author = book.Author;
            result.PublicationYear = book.PublicationYear;
            result.Isbn = book.Isbn;

            _ctx.Books.Update(result);
            await _ctx.SaveChangesAsync();

            //response
            response.Status = true;
            response.Data = result;
            response.Message = "Book update success!.";
            return response;
        }

        public async Task<BookDetailDto> DeleteBook(int id)
        {
            BookDetailDto response = new BookDetailDto()
            {
                Status = false,
                Message = "",
                Data = null
            };
            //Book result = books.Find(b => b.Id == id);
            //var result = _ctx.Books.Where(b => b.Id == id).FirstOrDefault();
            var result = _ctx.Books.Find(id);

            if (result == null)
            {
                response.Message = "No book available.";
                return response;
            }

            //delete book from list
            //books.Remove(result);
            _ctx.Books.Remove(result);
            await _ctx.SaveChangesAsync();

            //response
            response.Status = true;
            response.Data = result;
            response.Message = "Book delete success!";
            return response;
        }
    }
}

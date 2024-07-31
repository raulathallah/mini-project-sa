
using LibraryManagementSystemAPI.Dto;
using LibraryManagementSystemAPI.Interface;
using LibraryManagementSystemAPI.Models;


namespace LibraryManagementSystemAPI.Services
{
    public class BookService : IBookService
    {
        private static List<Book> books = new List<Book>();

        public BookListDto GetAllBook()
        {
            BookListDto response = new BookListDto()
            {
                Data = null,
                Status = false,
                Message = ""
            };
            if (books.Count == 0)
            {
                response.Message = "Book list is empty.";
                return response;
            }

            response.Data = books;
            response.Status = true;
            response.Message = "Get all book success!";
            return response;
        }

        public BookDetailDto GetBookById(int id)
        {
            BookDetailDto response = new BookDetailDto()
            {
                Status = false,
                Message = "",
                Data = null
            };

            Book res = books.FirstOrDefault(x => x.Id == id);

            if(res == null)
            {
                response.Message = "No book available.";
            }

            response.Status = true;
            response.Data = res;
            response.Message = "Get book by ID success!";
            return response;
        }

        public BookDetailDto AddNewBook(BookAddDto book)
        {
            BookDetailDto response = new BookDetailDto()
            {
                Status = false,
                Message = "",
                Data = null
            };


            if (books.Find(b => b.Title == book.Title) != null)
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
            books.Add(result);

            //response
            response.Status = true;
            response.Data = result;
            response.Message = "Add book success!";
            return response;
        }
        
        public BookDetailDto UpdateBook(int id, BookAddDto book)
        {
            BookDetailDto response = new BookDetailDto()
            {
                Status = false,
                Message = "",
                Data = null
            };

            Book result = books.Find(b => b.Id == id);
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

            //response
            response.Status = true;
            response.Data = result;
            response.Message = "Book update success!.";
            return response;
        }

        public BookDetailDto DeleteBook(int id)
        {
            BookDetailDto response = new BookDetailDto()
            {
                Status = false,
                Message = "",
                Data = null
            };
            Book result = books.Find(b => b.Id == id);
            if(result == null)
            {
                response.Message = "No book available.";
                return response;
            }

            //delete book from list
            books.Remove(result);

            //response
            response.Status = true;
            response.Data = result;
            response.Message = "Book delete success!";
            return response;
        }
    }
}

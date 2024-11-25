using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.Core.Models.Responses;
using LibraryManagementSystem.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Mappers
{
    public static class BookMappers
    {
        public static BookDetailResponse ToBookResponse(this Book bookModel)
        {
            return new BookDetailResponse()
            {
                Category = bookModel.Category,
                Description = bookModel.Description,
                Isbn = bookModel.Isbn,
                Publisher = bookModel.Publisher,
                Title = bookModel.Title,
                Language = bookModel.Language,
                Author = bookModel.Author,
                BookId = bookModel.BookId,
                DeleteReason = bookModel.DeleteReason,
                IsDeleted = bookModel.IsDeleted,
                Price = bookModel.Price,
                Stock = bookModel.Stock
            };
        }

        public static BookSearchResponse ToBookSearchResponse(this Book bookModel, string[]? locations)
        {
            return new BookSearchResponse()
            {
                BookId = bookModel.BookId,
                Category = bookModel.Category,
                Description = bookModel.Description,
                Isbn = bookModel.Isbn,
                Author = bookModel.Author,
                Publisher = bookModel.Publisher,
                Title = bookModel.Title,
                Locations = locations,
            };

        }

    }
}

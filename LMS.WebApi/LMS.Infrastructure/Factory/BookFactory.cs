using LMS.Core.Models;
using LMS.Core.Models.Requests;
using LMS.Domain.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Factory
{
    public class BookFactory
    {
        public static Book CreateBook(AddBookRequest request)
        {
            return new Book()
            {
                Title = request.Title,
                Author = request.Author,
                Isbn = request.Isbn,
                Publicationyear = request.PublicationYear
            };
        }
    }
}

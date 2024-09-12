using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.Domain.Models.Entities;
using LibraryManagementSystem.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Mappers
{
    public static class CheckOutMappers
    {
        public static BookCheckOutResponse ToCheckOutResponse(this BookUserTransactions model)
        {
            return new BookCheckOutResponse()
            {
                DueDate = model.DueDate,
                Isbn = model.Isbn,
                Title = model.Title,
            };
        }

        public static BookCheckOutLibrarianBookResponse ToLibrarianCheckOutBookResponse(this BookUserTransactions model)
        {
            return new BookCheckOutLibrarianBookResponse()
            {
                Isbn = model.Isbn,
                Title = model.Title,
                DueDate = model.DueDate,
                BorrowedAt = model.BorrowedAt,
                Location = model.LocationId,
            };
        }

        public static BookCheckOutLibrarianUserResponse ToLibrarianCheckOutUserResponse(this User model, int currentBook)
        {
            return new BookCheckOutLibrarianUserResponse()
            {
                FName = model.FName,
                LName = model.LName,
                LibraryCardNumber = model.LibraryCardNumber,
                LibraryCardExpiredDate = model.LibraryCardExpiredDate,
                Penalty = 0,
                CurrentBook = currentBook
            };
        }
    }
}

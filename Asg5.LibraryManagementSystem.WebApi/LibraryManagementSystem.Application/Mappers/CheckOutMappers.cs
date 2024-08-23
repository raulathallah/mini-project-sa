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
                UserId= model.UserId,
                BookId= model.BookId,
                BorrowedCurrentlyCount = 0,
                Penalty = 0
            };
        }
    }
}

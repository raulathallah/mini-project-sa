using LMS.Core.Models.Requests;
using LMS.Domain.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.Services
{
    public interface IBookManager
    {
        Task<object> BorrowBook(BorrowBookRequest request);
        Task<object> ReturnBook(ReturnBookRequest request);
    }
}

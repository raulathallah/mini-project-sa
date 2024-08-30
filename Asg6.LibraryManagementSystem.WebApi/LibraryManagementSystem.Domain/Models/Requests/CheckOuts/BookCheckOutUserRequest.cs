using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models.Requests.CheckOuts
{
    public class BookCheckOutUserRequest
    {
        public string? LibraryCardNumber { get; set; }
    }
}

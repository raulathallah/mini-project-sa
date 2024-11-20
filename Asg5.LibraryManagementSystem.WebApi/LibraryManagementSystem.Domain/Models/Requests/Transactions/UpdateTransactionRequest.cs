using LibraryManagementSystem.Domain.Models.Entities;
using LMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models.Requests.Transactions
{
    public class UpdateTransactionRequest
    {
        public List<BookUserTransactions> Transactions { get; set; }

    }
}

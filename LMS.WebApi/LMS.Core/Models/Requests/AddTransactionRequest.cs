using LMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.Models.Requests
{
    public class AddTransactionRequest
    {
        public List<Bookusertransaction> Transactions { get; set; }
    }
}

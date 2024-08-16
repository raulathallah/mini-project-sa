using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Responses.Base
{
    public class BaseResponse
    {
        public string Message { get; set; }
        public bool Status { get; set; }
    }
}

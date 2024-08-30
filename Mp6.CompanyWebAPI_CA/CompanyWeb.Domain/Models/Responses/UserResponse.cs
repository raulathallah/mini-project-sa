using CompanyWeb.Domain.Models.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Responses
{
    public class UserResponse : BaseResponse
    {
        public string? AppUserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
    }
}

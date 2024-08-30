using CompanyWeb.Domain.Models.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models.Responses
{
    public class IssueAccessTokenResponse
    {
        public string? Token { get; set; }
        public DateTime? ExpiredOn { get; set; }
    }
}

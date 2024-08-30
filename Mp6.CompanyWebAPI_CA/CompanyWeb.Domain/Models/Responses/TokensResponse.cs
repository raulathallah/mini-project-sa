using LibraryManagementSystem.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Responses
{
    public class TokensResponse
    {
        public IssueAccessTokenResponse? AccessToken { get; set; }
        public string? NewRefreshToken { get; set; }
    }
}

using CompanyWeb.Domain.Models.Auth;
using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Responses.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Responses
{
    public class AppUserResponse
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? ExpiredOn { get; set; }
        public DateTime? RefreshTokenExpiredOn { get; set; }

        //tambahan
        public object? User { get; set; }
        public string[]? Roles { get; set; }
        public object? Employee { get; set; }
    }
}

using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models.Responses
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
        public UserDetailResponse? User { get; set; }
        public List<string>? Roles { get; set; }

        
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Auth
{
    public class AppUser : IdentityUser
    {
        [Column("refreshtoken")]
        public string? RefreshToken { get; set; }
        [Column("refreshtokenexpiredon")]
        public DateTime? RefreshTokenExpiredOn { get; set; }
    }
}

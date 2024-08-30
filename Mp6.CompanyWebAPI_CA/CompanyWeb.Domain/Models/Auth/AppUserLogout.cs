using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Auth
{
    public class AppUserLogout
    {
        [Required]
        public string RefreshToken { get; set; } = null!;
    }
}

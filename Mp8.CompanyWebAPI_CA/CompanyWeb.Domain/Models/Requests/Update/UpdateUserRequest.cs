using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Requests.Update
{
    public class UpdateUserRequest
    {
        [Required]
        public string UserName { get; set; } = null!; 
        [Required]
        public string CurrentPassword { get; set; } = null!;
        [Required]
        public string NewPassword { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}

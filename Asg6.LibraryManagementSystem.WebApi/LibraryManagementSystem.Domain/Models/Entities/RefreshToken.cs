using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models.Entities
{
    public class RefreshToken
    {
        [Key]
        public int RefreshTokenId { get; set; }
        public string? AppUserId { get; set; }
        public string? Token { get; set; }
        public string? Username { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}

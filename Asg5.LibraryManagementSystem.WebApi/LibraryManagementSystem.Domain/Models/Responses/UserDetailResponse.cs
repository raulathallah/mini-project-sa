using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models.Responses
{
    public class UserDetailResponse
    {
        public int UserId { get; set; }
        public string FName { get; set; } = null!;
        public string LName { get; set; } = null!;
        public string UserPosition { get; set; } = null!;
        public string UserPrivilage { get; set; } = null!;
        public string UserNotes { get; set; } = null!;
        public string LibraryCardNumber { get; set; } = null!;
        public DateOnly LibraryCardExpiredDate { get; set; }
    }
}

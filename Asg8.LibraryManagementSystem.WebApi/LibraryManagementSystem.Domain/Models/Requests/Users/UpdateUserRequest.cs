using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models.Requests.Users
{
    public class UpdateUserRequest
    {
        public string fName { get; set; } = null!;
        public string lName { get; set; } = null!;
        public string UserPosition { get; set; } = null!;
        public string UserPrivilage { get; set; } = null!;
        public string UserNotes { get; set; } = null!;
    }
}

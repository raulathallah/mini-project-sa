using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.Models.Requests
{
    public class UpdateUserRequest
    {
        public string? Username { get; set; }
        public string? Phonenumber { get; set; }
    }
}

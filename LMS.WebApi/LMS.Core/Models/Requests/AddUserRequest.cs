using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.Models.Requests
{
    public class AddUserRequest
    {
        public string? Username { get; set; }
        public string? Phonenumber { get; set; }
    }
}

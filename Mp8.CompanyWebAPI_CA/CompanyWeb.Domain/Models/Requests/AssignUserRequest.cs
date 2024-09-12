using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Requests
{
    public class AssignUserRequest
    {
        public string? AppUserId { get; set; }
        public string? RoleId { get; set; }
    }
}

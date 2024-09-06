using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models.Requests
{
    public class AddRoleRequest
    {
        public string RoleName { get; set; } = null!;
    }
}

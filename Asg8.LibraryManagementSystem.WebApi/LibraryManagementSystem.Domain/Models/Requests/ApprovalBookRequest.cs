using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models.Requests
{
    public class ApprovalBookRequest
    {
        public int BookRequestId { get; set; }
        public string? Approval {  get; set; }
        public string? Notes { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Requests
{
    public class EmployeeLeaveApprovalRequest
    {
        public int LeaveRequestId { get; set; }
        public string? Approval {  get; set; }
        public string? Notes {  get; set; }
    }
}

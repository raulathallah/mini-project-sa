using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Entities
{
    [Table("leaverequests")]
    public class LeaveRequest
    {
        [Key]
        [Column("leaverequestid")]
        public int LeaveRequestId { get; set; }

        [Column("employeeid")]
        public int Empno { get; set; }

        [Column("leavetype")]
        public string? LeaveType { get; set; }

        [Column("leavereason")]
        public string? LeaveReason { get; set; }

        [Column("startdate")]
        public DateOnly? StartDate { get; set; }
        [Column("enddate")]
        public DateOnly? EndDate { get; set; }

        [Column("processid")]
        public int ProcessId { get; set; }

        [Column("filename")]
        public string? FileName { get; set; }
    }
}

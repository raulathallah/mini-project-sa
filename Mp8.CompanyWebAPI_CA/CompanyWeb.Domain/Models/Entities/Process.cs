using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Entities
{
    [Table("process")]
    public class Process
    {
        [Key]
        [Column("processid")]
        public int ProcessId { get; set; }

        [Column("workflowid")]
        public int WorkflowId { get; set; }

        [Column("empno")]
        public int Empno { get; set; }

        [Column("requesterid")]
        public string? RequesterId { get; set; }

        [Column("status")]
        public string? Status { get; set; }

        [Column("currentstepid")]
        public int CurrentStepId { get; set; }

        [Column("requestdate")]
        public DateTime? RequestDate { get; set; }
    }
}

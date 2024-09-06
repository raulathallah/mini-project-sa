using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models.Entities
{
    [Table("workflowaction")]

    public class WorkflowAction
    {
        [Key]
        [Column("actionid")]
        public int ActionId { get; set; }

        [Column("processid")]
        public int ProcessId { get; set; }

        [Column("stepid")]
        public int StepId { get; set; }

        [Column("actorid")]
        public string? ActorId { get; set; }

        [Column("action")]
        public string? Action { get; set; }

        [Column("actiondate")]
        public DateTime? ActionDate { get; set; }

        [Column("comments")]
        [StringLength(255)]
        public string? Comments { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Entities
{
    [Table("workflowsequences")]

    public class WorkflowSequence
    {
        [Key]
        [Column("stepid")]
        public int StepId { get; set; }

        [Column("workflowid")]
        public int WorkflowId { get; set; }

        [Column("steporder")]
        public int StepOrder { get; set; }

        [Column("stepname")]
        [StringLength(255)]
        public string? StepName { get; set; }

        [Column("requiredrole")]
        [StringLength(255)]
        public string? RequiredRole { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Entities
{
    [Table("workflow")]

    public class Workflow
    {
        [Key]
        [Column("workflowid")]
        public int WorkflowId { get; set; }

        [Column("workflowname")]
        public string? WorkflowName { get; set; }

        [Column("description")]
        public string? Description { get; set; }
    }
}

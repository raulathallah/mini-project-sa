using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models.Entities
{
    [Table("nextsteprules")]
    public class NextStepRules
    {
        [Key]
        [Column("ruleid")]
        public int RuleId { get; set; }

        [Column("currentstepid")]
        public int CurrentStepId { get; set; }

        [Column("nextstepid")]
        public int NextStepId { get; set; }

        [Column("conditiontype")]
        [StringLength(100)]
        public string? ConditionType { get; set; }

        [Column("conditionvalue")]
        [StringLength(100)]
        public string? ConditionValue { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CompanyWeb.Domain.Models.Entities
{
    [PrimaryKey("EmpDependentId")]
    [Table("employeedependents")]
    public partial class EmployeeDependent
    {
        [Key]
        [Column("empdependentid")]
        public int EmpDependentId { get; set; }

        [Column("dependentempno")]
        public int DependentEmpno { get; set; }

        [Column("fname")]
        [StringLength(255)]
        public string Fname { get; set; } = null!;

        [Column("lname")]
        [StringLength(255)]
        public string Lname { get; set; } = null!;

        [Column("sex")]
        [StringLength(255)]
        public string Sex { get; set; } = null!;

        [Column("relation")]
        [StringLength(255)]
        public string Relation { get; set; } = null!;

        [Column("birthdate")]
        public DateOnly? BirthDate { get; set; }

    }
}

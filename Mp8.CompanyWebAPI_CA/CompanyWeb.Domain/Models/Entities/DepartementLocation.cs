using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Entities
{
    [PrimaryKey("LocationId", "Deptno")]
    [Table("departementlocations")]
    public partial class DepartementLocation
    {
        [Key]
        [Column("deptno")]
        public int Deptno { get; set; }

        [Key]
        [Column("locationid")]
        public int LocationId { get; set; }

        [ForeignKey("LocationId")]
        [InverseProperty("DepartementLocation")]
        public virtual Location LocationIdNavigation { get; set; } = null!;

        [ForeignKey("Deptno")]
        [InverseProperty("DepartementLocation")]
        public virtual Departement DeptnoNavigation { get; set; } = null!;


    }
}

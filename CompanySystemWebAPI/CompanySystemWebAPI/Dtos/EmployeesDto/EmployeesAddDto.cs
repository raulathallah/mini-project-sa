using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CompanySystemWebAPI.Dtos.EmployeesAddDto
{
    public class EmployeesAddDto
    {
        [Column("fname")]
        [StringLength(255)]
        public string Fname { get; set; } = null!;

        [Column("lname")]
        [StringLength(255)]
        public string Lname { get; set; } = null!;

        [Column("address")]
        [StringLength(255)]
        public string Address { get; set; } = null!;

        [Column("dob")]
        public DateOnly Dob { get; set; }

        [Column("sex")]
        [StringLength(255)]
        public string Sex { get; set; } = null!;

        [Column("position")]
        [StringLength(255)]
        public string Position { get; set; } = null!;

        [AllowNull]
        [Column("deptno")]
        public int? Deptno { get; set; }
    }
}

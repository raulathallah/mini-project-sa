using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CompanyWeb.Domain.Models.Entities;

[PrimaryKey("Deptno")]
[Table("departements")]
[Index("Mgrempno", Name = "departements_mgrempno_key", IsUnique = true)]
public partial class Departement
{
    [Key]
    [Column("deptno")]
    public int Deptno { get; set; }

    [Required]
    [Column("deptname")]
    [StringLength(255)]
    public string Deptname { get; set; } = null!;
    
    [Column("mgrempno")]
    public int? Mgrempno { get; set; }

    [InverseProperty("DeptnoNavigation")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [ForeignKey("Mgrempno")]
    [InverseProperty("Departement")]
    public virtual Employee? MgrempnoNavigation { get; set; }

    [InverseProperty("DeptnoNavigation")]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    [InverseProperty("DeptnoNavigation")]
    public virtual ICollection<DepartementLocation> DepartementLocation { get; set; } = new List<DepartementLocation>();
}

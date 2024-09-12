using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CompanyWeb.Domain.Models.Entities;

[PrimaryKey("Empno")]
[Table("employees")]
public partial class Employee
{
    [Key]
    [Column("empno")]
    public int Empno { get; set; }

    [Column("appuserid")]
    public string? AppUserId { get; set; }

    [Column("directsupervisor")]
    public int? DirectSupervisor { get; set; }

    [Required]
    [Column("fname")]
    [StringLength(255)]
    public string Fname { get; set; } = null!;

    [Required]
    [Column("lname")]
    [StringLength(255)]
    public string Lname { get; set; } = null!;

    [Required]
    [Column("emplevel")]
    public int EmpLevel { get; set; }

    [Required]
    [Column("emptype")]
    public string EmpType { get; set; } = null!;

    [Required]
    [Column("address")]
    [StringLength(255)]
    public string Address { get; set; } = null!;

    [Required]
    [Column("phonenumber")]
    [StringLength(15)]
    public string PhoneNumber { get; set; } = null!;

    [Required]
    [Column("email")]
    [EmailAddress]
    [StringLength(255)]
    public string EmailAddress { get; set; } = null!;

    [Required]
    [Column("dob")]
    public DateOnly Dob { get; set; }

    [Required]
    [Column("sex")]
    [StringLength(255)]
    public string Sex { get; set; } = null!;

    [Required]
    [Column("position")]
    [StringLength(255)]
    public string Position { get; set; } = null!;

    [Required]
    [Column("ssn")]
    [StringLength(15)]
    public string Ssn { get; set; } = null!;

    [Required]
    [Column("salary")]
    public int Salary { get; set; }

    [Column("deactivatereason")]
    [StringLength(255)]
    public string? DeactivateReason { get; set; }

    [Column("updatedat")]
    public DateTime? UpdatedAt { get; set; }

    [Column("createdat")]
    public DateTime? CreatedAt { get; set; }

    [Column("isactive")]
    public bool IsActive { get; set; }

    [Column("deptno")]
    public int? Deptno { get; set; }

    [InverseProperty("MgrempnoNavigation")]
    public virtual Departement? Departement { get; set; }

    [ForeignKey("Deptno")]
    [InverseProperty("Employees")]
    public virtual Departement? DeptnoNavigation { get; set; }

    [InverseProperty("EmpnoNavigation")]
    public virtual ICollection<Workson> Worksons { get; set; } = new List<Workson>();
}

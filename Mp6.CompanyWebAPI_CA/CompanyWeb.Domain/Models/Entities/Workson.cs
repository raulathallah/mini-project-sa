using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CompanyWeb.Domain.Models.Entities;

[PrimaryKey("Empno", "Projno")]
[Table("worksons")]
public partial class Workson
{
    [Key]
    [Column("empno")]
    public int Empno { get; set; }

    [Key]
    [Column("projno")]
    public int Projno { get; set; }

    [Column("dateworked")]
    public DateOnly Dateworked { get; set; }

    [Column("hoursworked")]
    public int Hoursworked { get; set; }

    [ForeignKey("Empno")]
    [InverseProperty("Worksons")]
    public virtual Employee EmpnoNavigation { get; set; } = null!;

    [ForeignKey("Projno")]
    [InverseProperty("Worksons")]
    public virtual Project ProjnoNavigation { get; set; } = null!;
}

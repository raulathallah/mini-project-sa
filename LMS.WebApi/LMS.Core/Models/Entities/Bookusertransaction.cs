using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Models;


[Table("bookusertransactions")]
public partial class Bookusertransaction
{
    [Key]
    [Column("transactionid")]
    public int Transactionid { get; set; }

    [Column("userid")]
    public int Userid { get; set; }

    [Column("bookid")]
    public int Bookid { get; set; }

    [Column("borrowdate")]
    public DateOnly Borrowdate { get; set; }

    [Column("borrowexpired")]
    public DateOnly Borrowexpired { get; set; }

    [Column("isreturned")]
    public bool? Isreturned { get; set; }

    [Column("returndate")]
    public DateOnly Returndate { get; set; }

    [ForeignKey("Bookid")]
    public virtual Book Book { get; set; } = null!;

    [ForeignKey("Userid")]
    public virtual User User { get; set; } = null!;
}

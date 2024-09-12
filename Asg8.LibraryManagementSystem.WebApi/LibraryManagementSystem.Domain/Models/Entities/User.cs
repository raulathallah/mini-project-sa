using LibraryManagementSystem.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Core.Models;

[Table("users")]
public partial class User
{
    [Key]
    [Column("userid")]
    public int UserId { get; set; }

    [Column("appuserid")]
    public string? AppUserId { get; set; }

    [Column("fname")]
    [StringLength(255)]
    public string FName { get; set; } = null!;

    [Column("lname")]
    [StringLength(255)]
    public string LName { get; set; } = null!;

    [Column("userposition")]
    [StringLength(255)]
    public string UserPosition { get; set; } = null!;

    [Column("userprivilege")]
    [StringLength(255)]
    public string UserPrivilage { get; set; } = null!;

    [Column("librarycardnumber")]
    [StringLength(255)]
    public string LibraryCardNumber { get; set; } = null!;

    [Column("librarycardexpireddate")]
    [StringLength(255)]
    public DateOnly LibraryCardExpiredDate { get; set; }

    [Column("notes")]
    [StringLength(255)]
    public string? UserNotes { get; set; }

    [InverseProperty("UserIdNavigation")]
    public virtual ICollection<BookUserTransactions> BookUserTransactions { get; set; } = new List<BookUserTransactions>();
}

using LibraryManagementSystem.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Core.Models;

[Table("books")]
public partial class Book
{
    [Key]
    [Column("bookid")]
    public int BookId { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string Title { get; set; } = null!;

    [Column("category")]
    [StringLength(255)]
    public string Category { get; set; } = null!;

    [Column("author")]
    [StringLength(255)]
    public string Author { get; set; } = null!;

    [Column("publisher")]
    [StringLength(255)]
    public string Publisher { get; set; } = null!;

    [Column("isbn")]
    [StringLength(255)]
    public string Isbn { get; set; } = null!;

    [Column("description")]
    [StringLength(255)]
    public string Description { get; set; } = null!;

    [Column("deletereason")]
    [StringLength(255)]
    public string DeleteReason { get; set; } = null!;

    [Column("price")]
    [Range(0, int.MaxValue)]
    public int Price { get; set; }

    [Column("stock")]
    [Range(0, int.MaxValue)]
    public int Stock { get; set; }

    [Column("isdeleted")]
    public bool IsDeleted { get; set; }

    [Column("language")]
    [StringLength(255)]
    public string Language { get; set; } = null!;

    [InverseProperty("BookIdNavigation")]
    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();

    [InverseProperty("BookIdNavigation")]
    public virtual ICollection<BookUserTransactions> BookUserTransactions { get; set; } = new List<BookUserTransactions>();
}

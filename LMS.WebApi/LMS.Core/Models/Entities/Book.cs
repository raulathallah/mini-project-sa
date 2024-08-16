using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Models;

[Table("books")]
public partial class Book
{
    [Key]
    [Column("bookid")]
    public int Bookid { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string Title { get; set; } = null!;

    [Column("author")]
    [StringLength(255)]
    public string Author { get; set; } = null!;

    [Column("publicationyear")]
    public int Publicationyear { get; set; }

    [Column("isbn")]
    [StringLength(255)]
    public string Isbn { get; set; } = null!;
}

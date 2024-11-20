﻿using LibraryManagementSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models.Entities
{
    [Table("bookusertransactions")]
    public class BookUserTransactions
    {
        [Key]
        [Column("bookusertransactionid")]
        public int BookUserTransactionId { get; set; }

        [Column("bookid")]
        public int BookId { get; set; }

        [Column("userid")]
        public int UserId { get; set; }

        [Column("locationid")]
        public int LocationId { get; set; }

        [Column("title")]
        [StringLength(255)]
        public string Title { get; set; } = null!;

        [Column("isbn")]
        [StringLength(255)]
        public string Isbn { get; set; } = null!;

        [Column("duedate")]
        [StringLength(255)]
        public DateOnly DueDate { get; set; }

        [Column("isreturned")]
        public bool? IsReturned { get; set; }

        [Column("returndate")]
        public DateOnly ReturnDate { get; set; }

        [Column("borrowedAt")]
        public DateOnly BorrowedAt { get; set; }

        [ForeignKey("LocationId")]
        [InverseProperty("BookUserTransactions")]
        public virtual Location? LocationIdNavigation { get; set; }

        [ForeignKey("BookId")]
        [InverseProperty("BookUserTransactions")]
        public virtual Book? BookIdNavigation { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("BookUserTransactions")]
        public virtual User? UserIdNavigation { get; set; }
    }
}

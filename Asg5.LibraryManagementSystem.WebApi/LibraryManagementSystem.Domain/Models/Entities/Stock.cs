using LibraryManagementSystem.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models.Entities
{
    [PrimaryKey("BookId", "LocationId")]
    [Table("stocks")]
    public partial class Stock
    {
        [Key]
        [Column("bookid")]
        public int BookId { get; set; }

        [Key]
        [Column("locationid")]
        public int LocationId { get; set; }

        [Column("stocks")]
        public int StockCount { get; set; }

        [ForeignKey("BookId")]
        [InverseProperty("Stocks")]
        public virtual Book BookIdNavigation { get; set; } = null!;

        [ForeignKey("LocationId")]
        [InverseProperty("Stocks")]
        public virtual Location LocationIdNavigation { get; set; } = null!;
    }
}

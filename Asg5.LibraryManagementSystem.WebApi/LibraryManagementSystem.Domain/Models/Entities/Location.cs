using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models.Entities
{

    [Table("locations")]
    public partial class Location
    {
        [Key]
        [Column("locationid")]
        public int LocationId { get; set; }

        [Column("locationname")]
        [StringLength(255)]
        public string LocationName { get; set; } = null!;

        [InverseProperty("LocationIdNavigation")]
        public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();

        [InverseProperty("LocationIdNavigation")]
        public virtual ICollection<BookUserTransactions> BookUserTransactions { get; set; } = new List<BookUserTransactions>();
    }
}

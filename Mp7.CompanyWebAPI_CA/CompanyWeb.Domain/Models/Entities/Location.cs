using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Entities
{
    [PrimaryKey("LocationId")]
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
        public virtual ICollection<DepartementLocation> DepartementLocation { get; set; } = new List<DepartementLocation>();
    }
}

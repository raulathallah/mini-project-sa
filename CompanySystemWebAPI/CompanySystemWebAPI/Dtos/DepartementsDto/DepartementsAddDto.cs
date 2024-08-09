using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CompanySystemWebAPI.Dtos.DepartementsDto
{
    public class DepartementsAddDto
    {
        [Column("deptname")]
        [StringLength(255)]
        public string Deptname { get; set; } = null!;

        [AllowNull]
        [Column("mgrempno")]
        public int? Mgrempno { get; set; }
    }
}

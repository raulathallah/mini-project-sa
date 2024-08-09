using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompanySystemWebAPI.Dtos.WorksOnDto
{
    public class WorksOnAddDto
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
    }
}

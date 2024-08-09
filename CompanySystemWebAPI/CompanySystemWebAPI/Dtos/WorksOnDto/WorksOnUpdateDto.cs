using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompanySystemWebAPI.Dtos.WorksOnDto
{
    public class WorksOnUpdateDto
    {
        [Column("dateworked")]
        public DateOnly Dateworked { get; set; }

        [Column("hoursworked")]
        public int Hoursworked { get; set; }
    }
}

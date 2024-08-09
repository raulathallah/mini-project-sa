using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompanySystemWebAPI.Dtos.ProjectsDto
{
    public class ProjectsAddDto
    {
        [Column("projname")]
        [StringLength(255)]
        public string Projname { get; set; } = null!;

        [Column("deptno")]
        public int Deptno { get; set; }
    }
}

using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Application.Mappers
{
    public static class ProjectMappers
    {
        public static ProjectResponse ToProjectResponse(this Project project)
        {
            return new ProjectResponse()
            {
                Deptno = project.Deptno,
                ProjLocation = project.ProjLocation,
                Projname = project.Projname,
                Projno = project.Projno
            };
        }
    }
}

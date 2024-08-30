using CompanyWeb.Domain.Models.Dtos;
using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Application.Mappers
{
    public static class WorksOnMappers
    {
        public static WorksOnDetailDto ToWorksOnResponse(this Workson workson)
        {
            return new WorksOnDetailDto()
            {
                 Dateworked = workson.Dateworked,
                 Empno = workson.Empno,
                 Hoursworked = workson.Hoursworked,
                 Projno = workson.Projno
            };
        }
    }
}

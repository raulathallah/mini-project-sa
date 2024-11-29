using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Application.Mappers
{
    public static class DepartementMappers
    {
        public static DepartementDetailResponse ToDepartementDetailResponse(this Departement departement, List<int> locations)
        {
            return new DepartementDetailResponse()
            {
                Deptname = departement.Deptname,
                Deptno = departement.Deptno,
                Mgrempno = departement.Mgrempno,
                Location = locations
            };
        }
    }
}

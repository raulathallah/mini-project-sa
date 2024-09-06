using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Repositories
{
    public interface IDepartementLocationRepository
    {
        Task<DepartementLocation> Create(DepartementLocation departementLocation);
        Task<DepartementLocation> Update(DepartementLocation departementLocation);
        Task<List<DepartementLocation>> Delete(int deptNo);
        Task<List<DepartementLocation>> GetDepartementLocations(int pageNumber, int perPage);
        Task<DepartementLocation> GetDepartementLocation(int id);
        Task<IQueryable<DepartementLocation>> GetAllDepartementLocations();
    }
}

using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Repositories;
using LMS.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Infrastructure.Repositories
{
    public class DepartementLocationRepository : IDepartementLocationRepository
    {
        private readonly CompanyDbContext Context;
        public DepartementLocationRepository(CompanyDbContext context)
        {
            Context = context;
        }

        public async Task<DepartementLocation> Create(DepartementLocation departementLocation)
        {
            Context.DepartementLocations.Add(departementLocation);
            await Context.SaveChangesAsync();
            return departementLocation;
        }

        public async Task<List<DepartementLocation>> Delete(int deptNo)
        {
            var locations = await Context.DepartementLocations.Where(w => w.Deptno == deptNo).ToListAsync();
            Context.DepartementLocations.RemoveRange(locations);
            await Context.SaveChangesAsync();
            return locations;
        }

        public async Task<IQueryable<DepartementLocation>> GetAllDepartementLocations()
        {
            return Context.DepartementLocations;
        }

        public Task<DepartementLocation> GetDepartementLocation(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<DepartementLocation>> GetDepartementLocations(int pageNumber, int perPage)
        {
            throw new NotImplementedException();
        }

        public Task<DepartementLocation> Update(DepartementLocation departementLocation)
        {
            throw new NotImplementedException();
        }
    }
}

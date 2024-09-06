using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Repositories;
using LMS.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Infrastructure.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly CompanyDbContext Context;
        public LocationRepository(CompanyDbContext context)
        {
            Context = context;
        }
        public async Task<IQueryable<Location>> GetAllLocations()
        {
            return Context.Locations;
        }
    }
}

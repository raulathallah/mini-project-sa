using CompanyWeb.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Repositories
{
    public interface ILocationRepository
    {
        Task<IQueryable<Location>> GetAllLocations();
    }
}

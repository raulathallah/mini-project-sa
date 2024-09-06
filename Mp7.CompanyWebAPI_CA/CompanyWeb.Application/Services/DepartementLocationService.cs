using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Requests.Add;
using CompanyWeb.Domain.Repositories;
using CompanyWeb.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Application.Services
{
    public class DepartementLocationService : IDepartementLocationService
    {
        private readonly IDepartementLocationRepository _departementLocationRepository;

        public DepartementLocationService(IDepartementLocationRepository departementLocationRepository)
        {
            _departementLocationRepository = departementLocationRepository;
        }

        public async Task<DepartementLocation> Create(AddDepartementLocationRequest request)
        {
            var newDepartementLocation = new DepartementLocation()
            {
                Deptno = request.Deptno,
                LocationId = request.LocationId,
            };
            var response = await _departementLocationRepository.Create(newDepartementLocation);
            return response;
        }
    }
}

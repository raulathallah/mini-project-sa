using CompanyWeb.Application.Repositories;
using CompanyWeb.Application.Services;
using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Options;
using CompanyWeb.Domain.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Infrastructure.Services
{
    public class DepartementService : IDepartementService
    {
        private readonly IDepartementRepository _departementRepository;
        public DepartementService(IDepartementRepository departementRepository)
        {
            _departementRepository = departementRepository;
        }

        public async Task<Departement> CreateDepartement(AddDepartementRequest request)
        {
            return await _departementRepository.Create(request);
        }

        public async Task<Departement> DeleteDepartement(int id)
        {
            return await _departementRepository.Delete(id);
        }

        public async Task<Departement> GetDepartement(int id)
        {
            return await _departementRepository.GetDepartement(id);
        }

        public async Task<List<Departement>> GetDepartements(int pageNumber, int perPage)
        {
            return await _departementRepository.GetDepartements(pageNumber, perPage);
        }

        public async Task<Departement> UpdateDepartement(int id, UpdateDepartementRequest request)
        {
            return await _departementRepository.Update(id, request);
        }
    }
}

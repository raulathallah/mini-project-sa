using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Application.Services
{
    public interface IDepartementService
    {
        Task<Departement> CreateDepartement(AddDepartementRequest request);
        Task<Departement> UpdateDepartement(int id, UpdateDepartementRequest request);
        Task<Departement> DeleteDepartement(int id);
        Task<List<Departement>> GetDepartements(int pageNumber, int perPage);
        Task<Departement> GetDepartement(int id);
    }
}

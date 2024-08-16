using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Requests;
using CompanyWeb.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Application.Repositories
{
    public interface IDepartementRepository
    {
        Task<Departement> Create(AddDepartementRequest request);
        Task<Departement> Update(int id, UpdateDepartementRequest request);
        Task<Departement> Delete(int id);
        Task<List<Departement>> GetDepartements(int pageNumber, int perPage);
        Task<IQueryable<Departement>> GetAllDepartements();
        Task<int> GetDepartementIdByName(string deptName);
        Task<Departement> GetDepartement(int id);
    }
}

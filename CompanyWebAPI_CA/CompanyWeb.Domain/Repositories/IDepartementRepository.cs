using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Requests;
using CompanyWeb.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Repositories
{
    public interface IDepartementRepository
    {
        Task<Departement> Create(Departement departement);
        Task<Departement> Update(Departement departement);
        Task<Departement> Delete(int id);
        Task<IQueryable<Departement>> GetDepartements(int pageNumber, int perPage);
        Task<IQueryable<Departement>> GetAllDepartements();
        Task<int> GetDepartementIdByName(string deptName);
        Task<Departement> GetDepartement(int id);
    }
}

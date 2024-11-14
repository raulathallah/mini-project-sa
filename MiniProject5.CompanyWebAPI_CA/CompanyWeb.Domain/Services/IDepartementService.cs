using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Requests;
using CompanyWeb.Domain.Models.Requests.Add;
using CompanyWeb.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Services
{
    public interface IDepartementService
    {
        Task<object> CreateDepartement(AddDepartementRequest request);
        Task<object> UpdateDepartement(int id, UpdateDepartementRequest request);
        Task<object> DeleteDepartement(int id);
        Task<List<object>> GetDepartements(int pageNumber, int perPage);

        // NEW ======>
        Task<List<object>> GetAllDepartements();
        Task<object> GetDepartement(int id);
    }
}

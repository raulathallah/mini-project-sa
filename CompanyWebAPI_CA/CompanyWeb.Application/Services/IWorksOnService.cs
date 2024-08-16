using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Application.Services
{
    public interface IWorksOnService
    {
        Task<object> CreateWorksOn(AddWorksOnRequest request);
        Task<Workson> UpdateWorksOn(int projNo, int empNo, UpdateWorksOnRequest request);
        Task<Workson> DeleteWorksOn(int projNo, int empNo);
        Task<List<Workson>> GetWorksons(int pageNumber, int perPage);
        Task<Workson> GetWorkson(int projNo, int empNo);

    }
}

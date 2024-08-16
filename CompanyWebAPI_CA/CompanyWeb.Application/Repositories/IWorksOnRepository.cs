using CompanyWeb.Domain.Models.Dtos;
using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Application.Repositories
{
    public interface IWorksOnRepository
    {
        Task<WorksOnDetailDto> Create(AddWorksOnRequest request);
        Task<Workson> Update(int projNo, int empNo, UpdateWorksOnRequest request);
        Task<Workson> Delete(int projNo, int empNo);
        Task<List<Workson>> GetWorksons(int pageNumber, int perPage);
        Task<IQueryable<Workson>> GetAllWorksons();
        Task<int> GetProjectTotalHoursByProjectNumber(int projNo);
        Task<int> GetProjectCountByEmployeeNumber(int empNo);
        Task<Workson> GetWorkson(int projNo, int empNo);
    }
}

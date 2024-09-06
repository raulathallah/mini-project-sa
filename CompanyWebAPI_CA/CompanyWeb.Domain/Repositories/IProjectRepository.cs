using CompanyWeb.Domain.Models.Dtos;
using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Requests;
using CompanyWeb.Domain.Models.Requests.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Repositories
{
    public interface IProjectRepository
    {
        Task<Project> Create(Project project);
        Task<Project> Update(Project project);
        Task<Project> Delete(int id);
        Task<List<Project>> GetProjects(int pageNumber, int perPage);
        Task<IQueryable<Project>> GetAllProjects();
        Task<int> GetProjectCountByDepartmentNumber(int deptNo);
        Task<Project> GetProject(int id);
    }
}

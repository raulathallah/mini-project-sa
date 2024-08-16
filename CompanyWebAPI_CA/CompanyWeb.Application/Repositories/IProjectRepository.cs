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
    public interface IProjectRepository
    {
        Task<ProjectDetailDto> Create(AddProjectRequest request);
        Task<Project> Update(int id, UpdateProjectRequest request);
        Task<Project> Delete(int id);
        Task<List<Project>> GetProjects(int pageNumber, int perPage);
        Task<IQueryable<Project>> GetAllProjects();
        Task<int> GetProjectCountByDepartmentNumber(int deptNo);
        Task<Project> GetProject(int id);
    }
}

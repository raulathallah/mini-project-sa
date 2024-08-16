using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Application.Services
{
    public interface IProjectService
    {
        Task<object> CreateProject(AddProjectRequest request);
        Task<Project> UpdateProject(int id, UpdateProjectRequest request);
        Task<Project> DeleteProject(int id);
        Task<List<Project>> GetProjects(int pageNumber, int perPage);
        Task<Project> GetProject(int id);

    }
}

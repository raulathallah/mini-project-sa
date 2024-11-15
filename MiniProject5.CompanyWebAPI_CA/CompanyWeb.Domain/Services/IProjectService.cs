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
    public interface IProjectService
    {
        Task<object> CreateProject(AddProjectRequest request);
        Task<object> UpdateProject(int id, UpdateProjectRequest request);
        Task<object> DeleteProject(int id);
        Task<List<ProjectResponse>> GetProjects(int pageNumber, int perPage);

        //NEW======>
        Task<List<ProjectResponse>> GetAllProject();
        Task<object> GetProject(int id);

    }
}

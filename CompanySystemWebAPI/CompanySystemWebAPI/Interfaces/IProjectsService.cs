using CompanySystemWebAPI.Dtos.ProjectsDto;
using CompanySystemWebAPI.Models;

namespace CompanySystemWebAPI.Interfaces
{
    public interface IProjectsService
    {
        Task<Project> Create(ProjectsAddDto proj);
        Task<Project> Update(int id, ProjectsAddDto proj);
        Task<Project> Delete(int id);
        Task<List<Project>> GetProjects(int pageNumber, int perPage);
        Task<Project> GetProject(int id);


        // f.List all projects that are managed by the planning department.
        Task<List<object>> GetPlanningDepartementProjects();

        // m.List all projects that are managed by the IT and the HR department.
        Task<List<object>> GetITAndHRProjects();

        // o.Retrieve all the female manager and the project that she managed
        Task<List<object>> GetFemaleManagerProjects();

    }
}

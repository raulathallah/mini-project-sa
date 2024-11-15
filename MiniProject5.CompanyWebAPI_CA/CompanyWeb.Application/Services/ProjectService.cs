
using CompanyWeb.Application.Mappers;
using CompanyWeb.Domain.Models.Dtos;
using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Options;
using CompanyWeb.Domain.Models.Requests;
using CompanyWeb.Domain.Models.Requests.Add;
using CompanyWeb.Domain.Models.Responses;
using CompanyWeb.Domain.Models.Responses.Base;
using CompanyWeb.Domain.Repositories;
using CompanyWeb.Domain.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Application.Services
{
    public class ProjectService : IProjectService
    {

        private readonly IProjectRepository _projectRepository;
        private readonly CompanyOptions _companyOptions;

        public ProjectService(IProjectRepository projectRepository,
            IOptions<CompanyOptions> companyOptions)
        {
            _projectRepository = projectRepository;
            _companyOptions = companyOptions.Value;
        }

        public async Task<object> CreateProject(AddProjectRequest request)
        {
            var response = CreateResponse();
            int projectCount = await _projectRepository.GetProjectCountByDepartmentNumber(request.Deptno);
            if (projectCount >= _companyOptions.MaxDepartementProject)
            {
                response.Message = $"Departement with ID {request.Deptno} can only hold 10 project maximum. ({projectCount}/10)";
                return response;
            }

            var p = await _projectRepository.GetAllProjects();
            var isAnyProject = p.Any(x => x.Projname == request.Projname);
            if (isAnyProject)
            {   
                //NEW======>
                response.Message = $"Project already exist";
                return response;
            }

            var newProj = new Project()
            {
                Projname = request.Projname,
                Deptno = request.Deptno,
                ProjLocation = request.ProjLocation
            };

            var data = await _projectRepository.Create(newProj);
            if(data == null)
            {
                response.Message = $"Project with name {request.Projname} already exist";
                return response;
            }
            response.Message = "Success";
            response.Status = true;
            response.Data = data.ToProjectResponse();
            return response; 
        }

        public async Task<object> DeleteProject(int id)
        {
            var response = await _projectRepository.Delete(id);
            return response.ToProjectResponse();
        }

        public async Task<object> GetProject(int id)
        {
            var response = await _projectRepository.GetProject(id);
            return response.ToProjectResponse();
        }

        public async Task<List<ProjectResponse>> GetProjects(int pageNumber, int perPage)
        {
            var response = await _projectRepository.GetProjects(pageNumber, perPage);
            return response.Select(s => s.ToProjectResponse()).ToList();

        }

        //NEW======>
        public async Task<List<ProjectResponse>> GetAllProject()
        {
            var response = await _projectRepository.GetAllProjects();
            return response.Select(s => s.ToProjectResponse()).ToList();

        }

        public async Task<object> UpdateProject(int id, UpdateProjectRequest request)
        {
            var res = CreateResponse();
            var project = await _projectRepository.GetProject(id);
            if (project == null)
            {
                //NEW======>
                res.Message = $"Project not found!";
                return res;
            }

            project.Projname = request.Projname;
            project.Deptno = request.Deptno;
            project.ProjLocation = request.ProjLocation;

            var p = await _projectRepository.GetAllProjects();

            //NEW======>
            var isAnyProject = p.Where(w=> w.Projno != id).Any(x => x.Projname == request.Projname);
            if (isAnyProject)
            {
                //NEW======>
                res.Message = $"Project already exist";
                return res;
            }

            var response = await _projectRepository.Update(project);
            return response.ToProjectResponse();
        }

        ProjectDetailResponse CreateResponse()
        {
            return new ProjectDetailResponse()
            {
                Status = false,
                Message = "",
                Data = null,
            };
        }
    }
}

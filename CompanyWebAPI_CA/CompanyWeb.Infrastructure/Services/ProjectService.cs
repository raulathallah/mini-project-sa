using CompanyWeb.Application.Repositories;
using CompanyWeb.Application.Services;
using CompanyWeb.Domain.Models.Dtos;
using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Options;
using CompanyWeb.Domain.Models.Requests;
using CompanyWeb.Domain.Models.Responses;
using CompanyWeb.Domain.Models.Responses.Base;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Infrastructure.Services
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

            var data = await _projectRepository.Create(request);
            if(data == null)
            {
                response.Message = $"Project with name {request.Projname} already exist";
                return response;
            }
            response.Message = "Success";
            response.Status = true;
            response.Data = data;
            return response; 
        }

        public async Task<Project> DeleteProject(int id)
        {
            return await _projectRepository.Delete(id);
        }

        public async Task<Project> GetProject(int id)
        {
            return await _projectRepository.GetProject(id);
        }

        public async Task<List<Project>> GetProjects(int pageNumber, int perPage)
        {
            return await _projectRepository.GetProjects(pageNumber, perPage);

        }

        public async Task<Project> UpdateProject(int id, UpdateProjectRequest request)
        {
            return await _projectRepository.Update(id, request);
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


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
using PdfSharpCore.Pdf;
using PdfSharpCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheArtOfDev.HtmlRenderer.Core;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CompanyWeb.Application.Services
{
    public class ProjectService : IProjectService
    {

        private readonly IProjectRepository _projectRepository;
        private readonly CompanyOptions _companyOptions;
        private readonly IPdfService _pdfService;
        private readonly IWorksOnRepository _worksOnRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public ProjectService(IProjectRepository projectRepository,
            IOptions<CompanyOptions> companyOptions,
            IPdfService pdfService,
            IWorksOnRepository worksOnRepository,
            IEmployeeRepository employeeRepository)
        {
            _projectRepository = projectRepository;
            _companyOptions = companyOptions.Value;
            _pdfService = pdfService;
            _worksOnRepository = worksOnRepository;
            _employeeRepository = employeeRepository;
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
                return null;
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

        //Generate Report
        public async Task<byte[]> GenerateProjectReportPDF()
        {
            string htmlcontent = String.Empty;
            var document = new PdfDocument();
            var config = new PdfGenerateConfig();
            config.PageOrientation = PageOrientation.Landscape;
            config.SetMargins(8);
            config.PageSize = PageSize.A4;
            string cssStr = File.ReadAllText(@"./style.css");
            CssData css = PdfGenerator.ParseStyleSheet(cssStr);


            var worksons = await _worksOnRepository.GetAllWorksons();
            var projects = await _projectRepository.GetAllProjects();

            var join = (from work in worksons
                       join proj in projects on work.Projno equals proj.Projno
                       select work)
                       .GroupBy(gb=>gb.Projno);

            htmlcontent += "<h1>Project Report</h3>";
            htmlcontent += "<hr></hr>";

            foreach (var item in join.ToList())
            {
                var getProject = await _projectRepository.GetProject(item.Key);
                var totalEmp = item.Select(s => s.Empno).Distinct().Count();
                var hourWorked = item.Select(s => s.Hoursworked);
                htmlcontent += "<p>Project No : " + item.Key + "</p>";
                htmlcontent += "<p>Project Name : " + getProject.Projname + "</p>";
                htmlcontent += "<p>Total Hours Logged : " + hourWorked.Sum() + "</p>";
                htmlcontent += "<p>Number of Employee Involved : " + totalEmp + "</p>";
                htmlcontent += "<p>Average Hour per Employee : " + hourWorked.Average() + "</p>";
                htmlcontent += "<hr></hr>";
            }

            return _pdfService.OnGeneratePDF(htmlcontent);
        }

        public async Task<object> GetProject(int id)
        {
            var response = await _projectRepository.GetProject(id);
            return response.ToProjectResponse();
        }


        public async Task<List<object>> GetAllProject()
        {
            var response = await _projectRepository.GetAllProjects();
            return response.ToList<object>();
        }

        // PROJECT REPORT JSON
        public async Task<List<object>> GetProjectReport()
        {
            var worksons = await _worksOnRepository.GetAllWorksons();
            var projects = await _projectRepository.GetAllProjects();

            var join = (from work in worksons
                        join proj in projects on work.Projno equals proj.Projno
                        select work)
                       .GroupBy(gb => gb.Projno)
                       .Select(s => new
                       {
                           Projno = s.Key,
                           Projname = s.Select(s1=>s1.ProjnoNavigation.Projname).FirstOrDefault(),
                           TotalHourLogged = s.Select(s1=>s1.Hoursworked).Sum(),
                           TotalEmpInvolved = s.Select(s1=>s1.Empno).Distinct().Count(),
                           AvgHourWorked = Math.Round(s.Select(s1=>s1.Hoursworked).Average(), 2)
                       })
                       .ToList<object>();

            return join;
        }

        public async Task<List<ProjectResponse>> GetProjects(int pageNumber, int perPage)
        {
            var response = await _projectRepository.GetProjects(pageNumber, perPage);
            return response.Select(s => s.ToProjectResponse()).ToList();

        }

        public async Task<object> UpdateProject(int id, UpdateProjectRequest request)
        {
            var project = await _projectRepository.GetProject(id);
            if (project == null)
            {
                return null;
            }

            project.Projname = request.Projname;
            project.Deptno = request.Deptno;
            project.ProjLocation = request.ProjLocation;

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


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
    public class WorksOnService : IWorksOnService
    {

        private readonly IWorksOnRepository _worksOnRepository;
        private readonly CompanyOptions _companyOptions;

        public WorksOnService(IWorksOnRepository worksOnRepository, IOptions<CompanyOptions> companyOptions)
        {
            _worksOnRepository = worksOnRepository;
            _companyOptions = companyOptions.Value;
        }

        public async Task<object> CreateWorksOn(AddWorksOnRequest request)
        {
            var response = CreateResponse();
            int workHourNow = await _worksOnRepository.GetProjectTotalHoursByProjectNumber(request.Projno);
            var total = workHourNow + request.Hoursworked;
            if (total > _companyOptions.MaxWorkingHoursPerProject)
            {
                response.Message = $"Maximum working hours for the project. ({workHourNow}/600)";
                return response;
            }
            int projectCount = await _worksOnRepository.GetProjectCountByEmployeeNumber(request.Empno);
            if (projectCount >= _companyOptions.MaxEmployeeProject)
            {
                response.Message = $"Maximum employee projects. ({projectCount}/3)";
                return response;
            }
            var data = await _worksOnRepository.Create(request);
            response.Status = true;
            response.Message = "Success";
            response.Data = data;
            return response;
        }

        public async Task<Workson> DeleteWorksOn(int projNo, int empNo)
        {
            return await _worksOnRepository.Delete(projNo, empNo);
        }

        public async Task<Workson> GetWorkson(int projNo, int empNo)
        {
            return await _worksOnRepository.GetWorkson(projNo, empNo);
        }

        public async Task<List<Workson>> GetWorksons(int pageNumber, int perPage)
        {
            return await _worksOnRepository.GetWorksons(pageNumber, perPage);
        }

        //NEW======>
        public async Task<List<Workson>> GetAllWorksons()
        {
            var response = await _worksOnRepository.GetAllWorksons();
            return response.ToList();
        }

        public async Task<Workson> UpdateWorksOn(int projNo, int empNo, UpdateWorksOnRequest request)
        {
            return await _worksOnRepository.Update(projNo, empNo, request);
        }

        WorksOnDetailResponse CreateResponse()
        {
            return new WorksOnDetailResponse()
            {
                Status = false,
                Message = "",
                Data = null,
            };
        }
    }
}

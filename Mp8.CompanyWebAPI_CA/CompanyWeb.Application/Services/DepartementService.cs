﻿
using CompanyWeb.Application.Mappers;
using CompanyWeb.Domain.Models.Auth;
using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Options;
using CompanyWeb.Domain.Models.Requests;
using CompanyWeb.Domain.Models.Requests.Add;
using CompanyWeb.Domain.Models.Responses;
using CompanyWeb.Domain.Repositories;
using CompanyWeb.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Application.Services
{
    public class DepartementService : IDepartementService
    {
        private readonly IDepartementRepository _departementRepository;
        private readonly IDepartementLocationRepository _departementLocationRepository;
        private readonly IDepartementLocationService _departementLocationService;
        private readonly ILocationRepository _locationRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DepartementService(IDepartementRepository departementRepository, 
            IDepartementLocationRepository departementLocationRepository, 
            IDepartementLocationService departementLocationService, 
            ILocationRepository locationRepository, 
            IEmployeeRepository employeeRepository, 
            UserManager<AppUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IHttpContextAccessor httpContextAccessor)
        {
            _departementRepository = departementRepository;
            _departementLocationRepository = departementLocationRepository;
            _departementLocationService = departementLocationService;
            _locationRepository = locationRepository;
            _employeeRepository = employeeRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<object> CreateDepartement(AddDepartementRequest request)
        {
            var departements = await _departementRepository.GetAllDepartements();
            var d = departements.Any(x => x.Deptname == request.Deptname);
            if (d)
            {
                return null;
            }
            var newDept = new Departement()
            {
                Deptname = request.Deptname,
                Mgrempno = request.Mgrempno,
            };
            if (request.Mgrempno == null)
            {
                newDept.Mgrempno = null;
            }
            var response = await _departementRepository.Create(newDept);

            foreach(var item in request.Location)
            {
                var newDepartementLocation = new AddDepartementLocationRequest()
                {
                    Deptno = response.Deptno,
                    LocationId = item
                };
                await _departementLocationService.Create(newDepartementLocation);
            }

            var dl = await _departementLocationRepository.GetAllDepartementLocations();
            return response.ToDepartementDetailResponse(dl.Where(w => w.Deptno == response.Deptno).Select(s2 => s2.LocationId).ToList());
        }

        public async Task<object> DeleteDepartement(int id)
        {
            var response = await _departementRepository.Delete(id);
            var dl = await _departementLocationRepository.GetAllDepartementLocations();

            return response.ToDepartementDetailResponse(dl.Where(w => w.Deptno == id).Select(s2 => s2.LocationId).ToList());
        }

        public async Task<object> GetDepartement(int id)
        {
            var locations = await _locationRepository.GetAllLocations();
            var dl = await _departementLocationRepository.GetAllDepartementLocations();
            var departement = await _departementRepository.GetDepartement(id);

            return departement
                .ToDepartementDetailResponse(dl.Where(w => w.Deptno == id).Select(s1 => s1.LocationId).ToList());
        }

        public async Task<List<object>> GetDepartements(int pageNumber, int perPage)
        {
            var locations = await _locationRepository.GetAllLocations();
            var dl = await _departementLocationRepository.GetAllDepartementLocations();
            var departement = await _departementRepository.GetDepartements(pageNumber, perPage);

            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var roles = await _userManager.GetRolesAsync(user);

            var allEmp = await _employeeRepository.GetAllEmployees();
            var reqEmp = allEmp.Where(w=>w.AppUserId == user.Id).FirstOrDefault();
            var listSupervisedDeptno = allEmp.Where(w => w.Deptno == reqEmp.Deptno).Select(s=>s.Deptno).Distinct();

            if (roles.Any(x => x == "Employee Supervisor"))
            {

                return departement
                    .Where(w => listSupervisedDeptno.Contains(w.Deptno))
                    .Select(s => s.ToDepartementDetailResponse(dl.Where(w => w.Deptno == s.Deptno).Select(s2 => s2.LocationId).ToList()))
                    .ToList<object>();
            }

            return departement
                .Select(s => s.ToDepartementDetailResponse(dl.Where(w => w.Deptno == s.Deptno).Select(s2 => s2.LocationId).ToList()))
                .ToList<object>();


            /*var l = (from value in departement
                    join deptLoc in dl on value.Deptno equals deptLoc.Deptno
                    join loc in locations on deptLoc.LocationId equals loc.LocationId
                    select deptLoc);
           return departement
               .Select(s1 => 
               s1.ToDepartementDetailResponse(l.Where(w=> w.Deptno == s1.Deptno)
               .Select(s2=>s2.LocationIdNavigation.LocationName)
               ))
               .ToList<object>();*/
        }

        public async Task<List<object>> GetAllDepartements()
        {
            var locations = await _locationRepository.GetAllLocations();
            var dl = await _departementLocationRepository.GetAllDepartementLocations();
            var departement = await _departementRepository.GetAllDepartements();

            return departement
                .Select(s => s.ToDepartementDetailResponse(dl.Where(w => w.Deptno == s.Deptno).Select(s2 => s2.LocationId).ToList()))
                .ToList<object>();
        }
        public async Task<object> UpdateDepartement(int id, UpdateDepartementRequest request)
        {
            var dept = await _departementRepository.GetDepartement(id);
            var emp = await _employeeRepository.GetEmployee(request.Mgrempno.GetValueOrDefault());
            if (dept == null)
            {
                return null;
            }
            dept.Deptname = request.Deptname;

            // check id yang akan dijadikan manager
            var user = await _userManager.FindByIdAsync(emp.AppUserId);
            var roles = await _userManager.GetRolesAsync(user);

            if(roles.Any(x=>x == "Department Manager"))
            {
                dept.Mgrempno = request.Mgrempno;
            }
            else
            {
                return new
                {
                    Status = false,
                    Message = "Employe is not a manager"
                };
            }


            // update dept location
            await _departementLocationRepository.Delete(id);
            foreach (var item in request.Location)
            {
                var newDepartementLocation = new AddDepartementLocationRequest()
                {
                    Deptno = id,
                    LocationId = item
                };
                await _departementLocationService.Create(newDepartementLocation);
            }

            var deptLocations = await _departementLocationRepository.GetAllDepartementLocations();
            // update location
            var response = await _departementRepository.Update(dept);
            return response.ToDepartementDetailResponse(deptLocations.Where(w=>w.Deptno == id).Select(s=>s.LocationId).ToList());
        }
    }
}

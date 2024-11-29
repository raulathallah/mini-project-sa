
using CompanyWeb.Application.Mappers;
using CompanyWeb.Domain.Models.Auth;
using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Requests;
using CompanyWeb.Domain.Repositories;
using LMS.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Infrastructure.Repositories
{
    public class DepartementRepository : IDepartementRepository
    {
        private readonly CompanyDbContext Context;
        private readonly ILocationRepository _locationRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartementLocationRepository _departementLocationRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DepartementRepository(CompanyDbContext context, ILocationRepository locationRepository, IEmployeeRepository employeeRepository, IDepartementLocationRepository departementLocationRepository, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            _locationRepository = locationRepository;
            _employeeRepository = employeeRepository;
            _departementLocationRepository = departementLocationRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Departement> Create(Departement departement)
        {
            Context.Departements.Add(departement);
            await Context.SaveChangesAsync();
            return departement;
        }

        public async Task<Departement> Delete(int id)
        {
            var d = await Context.Departements.FindAsync(id);
            if (d == null)
            {
                return null;
            }

            Context.Departements.Remove(d);
            await Context.SaveChangesAsync();

            return d;
        }

        public async Task<Departement> GetDepartement(int id)
        {
            var d = await Context.Departements.FindAsync(id);
            if (d == null)
            {
                return null;
            }
            return d;
        }

        public async Task<IQueryable<Departement>> GetDepartements(int pageNumber, int perPage)
        {
            var locations = await _locationRepository.GetAllLocations();
            var dl = await _departementLocationRepository.GetAllDepartementLocations();

            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var roles = await _userManager.GetRolesAsync(user);

            var allEmp = await _employeeRepository.GetAllEmployees();
            var reqEmp = allEmp.Where(w => w.AppUserId == user.Id).FirstOrDefault();
            var listSupervisedDeptno = allEmp.Where(w => w.Deptno == reqEmp.Deptno).Select(s => s.Deptno).Distinct();

            if (roles.Any(x => x == "Department Manager"))
            {

                return Context.Departements
                    .Where(w => w.Deptno == reqEmp.Deptno)
                    .AsQueryable<Departement>();
            }

            return Context.Departements
                    .OrderBy(ob => ob.Deptno)
                    .Skip((pageNumber - 1) * perPage)
                    .Take(perPage)
                    .AsQueryable<Departement>(); ;
        }

        public async Task<Departement> Update(Departement departement)
        {
            Context.Departements.Update(departement);
            await Context.SaveChangesAsync();
            return departement;
        }

        public async Task<IQueryable<Departement>> GetAllDepartements()
        {
            return Context.Departements;
        }

        public async Task<int> GetDepartementIdByName(string deptName)
        {
            return await Context.Departements
                .Where(w => w.Deptname == deptName)
                .Select(s => s.Deptno)
                .FirstOrDefaultAsync();
        }

    }
}

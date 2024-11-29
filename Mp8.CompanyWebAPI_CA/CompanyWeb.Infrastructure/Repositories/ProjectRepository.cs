using CompanyWeb.Domain.Models.Auth;
using CompanyWeb.Domain.Models.Dtos;
using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Requests;
using CompanyWeb.Domain.Models.Requests.Add;
using CompanyWeb.Domain.Repositories;
using LMS.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly CompanyDbContext Context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProjectRepository(CompanyDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Project> Create(Project project)
        {
            Context.Projects.Add(project);
            await Context.SaveChangesAsync();
            return project;
        }

        public async Task<Project> Delete(int id)
        {
            var p = await Context.Projects.FindAsync(id);
            if (p == null)
            {
                return null;
            }

            Context.Projects.Remove(p);
            await Context.SaveChangesAsync();

            return p;
        }

        public async Task<Project> GetProject(int id)
        {
            var proj = await Context.Projects.FindAsync(id);
            if (proj == null)
            {
                return null;
            }
            return proj;
        }

        public async Task<List<Project>> GetProjects(int pageNumber, int perPage)
        {

            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var roles = await _userManager.GetRolesAsync(user);


            var allEmp = Context.Employees;
            var userRequest = allEmp.Where(w => w.AppUserId == user.Id).FirstOrDefault();


            if (roles.Any(x => x == "Department Manager"))
            {
                return await Context.Projects
                .Where(w => w.Deptno == userRequest.Deptno).OrderBy(ob => ob.Projno)
                .Skip((pageNumber - 1) * perPage)
                .Take(perPage)
                .ToListAsync<Project>();
            }


            return await Context.Projects
                .OrderBy(ob => ob.Projno)
                .Skip((pageNumber - 1) * perPage)
                .Take(perPage)
                .ToListAsync<Project>();
        }

        public async Task<IQueryable<Project>> GetAllProjects()
        {
            return Context.Projects;
        }
        public async Task<Project> Update(Project project)
        {
            Context.Projects.Update(project);
            await Context.SaveChangesAsync();
            return project;
        }

        public async Task<int> GetProjectCountByDepartmentNumber(int deptNo)
        {
            return await Context.Projects.Where(w => w.Deptno == deptNo).CountAsync();
        }
    }
}

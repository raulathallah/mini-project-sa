using CompanySystemWebAPI.Dtos.ProjectsDto;
using CompanySystemWebAPI.Interfaces;
using CompanySystemWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.Intrinsics.Arm;

namespace CompanySystemWebAPI.Services
{
    public class ProjectsService : IProjectsService
    {

        private readonly CompanyContext _companyContext;

        public ProjectsService(CompanyContext companyContext)
        {
            _companyContext = companyContext;
        }
        public async Task<Project> Create(ProjectsAddDto proj)
        {
            var p = _companyContext.Projects.Any(x => x.Projname == proj.Projname);
            if (p)
            {
                return null;
            }

            var newProj = new Project()
            {
                Projname = proj.Projname,
                Deptno = proj.Deptno,
            };

            _companyContext.Projects.Add(newProj);
            await _companyContext.SaveChangesAsync();

            return newProj;
        }

        public async Task<Project> Delete(int id)
        {
            var p = await _companyContext.Projects.FindAsync(id);
            if (p == null)
            {
                return null;
            }

            _companyContext.Projects.Remove(p);
            await _companyContext.SaveChangesAsync();

            return p;
        }

        public async Task<Project> GetProject(int id)
        {
            var proj = await _companyContext.Projects.FindAsync(id);
            if (proj == null)
            {
                return null;
            }
            return proj;
        }

        public async Task<List<Project>> GetProjects(int pageNumber, int perPage)
        {
            return await _companyContext.Projects
                .OrderBy(ob => ob.Projno)
                .Skip((pageNumber - 1) * perPage)
                .Take(perPage)
                .ToListAsync<Project>();

        }

        public async Task<Project> Update(int id, ProjectsAddDto proj)
        {
            var p = await _companyContext.Projects.FindAsync(id);
            if (p == null)
            {
                return null;
            }

            p.Projname = proj.Projname;
            p.Deptno = proj.Deptno;

            _companyContext.Projects.Update(p);
            await _companyContext.SaveChangesAsync();

            return p;
        }

        public async Task<List<object>> GetPlanningDepartementProjects()
        {
            var proj = await (from value in _companyContext.Projects 
                       join dept in _companyContext.Departements on value.Deptno equals dept.Deptno
                       where dept.Deptname == "Planning" 
                       select value).ToListAsync<object>();

            return proj;
        }
        public async Task<List<object>> GetITAndHRProjects()
        {
            var proj = await (from value in _companyContext.Projects
                       join dept in _companyContext.Departements on value.Deptno equals dept.Deptno
                       where (dept.Deptname == "IT" || dept.Deptname == "HR")
                       select value)
                       .ToListAsync<object>();
            return proj;
        }
        public async Task<List<object>> GetFemaleManagerProjects()
        {
            var projects = from value in _companyContext.Employees
                           join dept in _companyContext.Departements on value.Empno equals dept.Mgrempno
                           join proj in _companyContext.Projects on dept.Deptno equals proj.Deptno
                           where (value.Sex == "Female")
                           group proj by value.Empno;

            var result = await projects
                .Select(s => new
                {
                    Name = s
                    .Select(n => n.DeptnoNavigation.MgrempnoNavigation.Fname + " " + n.DeptnoNavigation.MgrempnoNavigation.Lname)
                    .FirstOrDefault(),
                    Projects = s.ToList(),
                })
                .ToListAsync<object>();

            return result;
        }

        

    }
}

using CompanyWeb.Application.Repositories;
using CompanyWeb.Domain.Models.Dtos;
using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Requests;
using LMS.Infrastructure;
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
        public ProjectRepository(CompanyDbContext context)
        {
            Context = context;
        }

        public async Task<ProjectDetailDto> Create(AddProjectRequest proj)
        {
            var p = Context.Projects.Any(x => x.Projname == proj.Projname);
            if (p)
            {
                return null;
            }

            var newProj = new Project()
            {
                Projname = proj.Projname,
                Deptno = proj.Deptno,
            };

            Context.Projects.Add(newProj);
            await Context.SaveChangesAsync();

            return new ProjectDetailDto()
            {
                Projno = newProj.Projno,
                Projname = newProj.Projname,
                Deptno = newProj.Deptno,
                
            };
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
        public async Task<Project> Update(int id, UpdateProjectRequest proj)
        {
            var p = await Context.Projects.FindAsync(id);
            if (p == null)
            {
                return null;
            }

            p.Projname = proj.Projname;
            p.Deptno = proj.Deptno;

            Context.Projects.Update(p);
            await Context.SaveChangesAsync();

            return p;
        }

        public async Task<int> GetProjectCountByDepartmentNumber(int deptNo)
        {
            return await Context.Projects.Where(w => w.Deptno == deptNo).CountAsync();
        }
    }
}

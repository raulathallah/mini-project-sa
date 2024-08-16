
using CompanyWeb.Application.Repositories;
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
    public class DepartementRepository : IDepartementRepository
    {
        private readonly CompanyDbContext Context;

        public DepartementRepository(CompanyDbContext context)
        {
            Context = context;
        }
        public async Task<Departement> Create(AddDepartementRequest request)
        {
            var d = Context.Departements.Any(x => x.Deptname == request.Deptname);
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
            Context.Departements.Add(newDept);
            await Context.SaveChangesAsync();
            return newDept;
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

        public async Task<List<Departement>> GetDepartements(int pageNumber, int perPage)
        {
            return await Context.Departements
                    .OrderBy(ob => ob.Deptno)
                    .Skip((pageNumber - 1) * perPage)
                    .Take(perPage)
                    .ToListAsync<Departement>();
        }

        public async Task<Departement> Update(int id, UpdateDepartementRequest request)
        {
            var d = await Context.Departements.FindAsync(id);
            if (d == null)
            {
                return null;
            }
            d.Deptname = request.Deptname;
            d.Mgrempno = request.Mgrempno;

            Context.Departements.Update(d);
            await Context.SaveChangesAsync();

            return d;
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

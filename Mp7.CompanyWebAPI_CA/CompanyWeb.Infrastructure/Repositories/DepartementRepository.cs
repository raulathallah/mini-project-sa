
using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Requests;
using CompanyWeb.Domain.Repositories;
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

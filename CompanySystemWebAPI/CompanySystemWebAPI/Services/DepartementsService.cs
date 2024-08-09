using CompanySystemWebAPI.Dtos.DepartementsDto;
using CompanySystemWebAPI.Interfaces;
using CompanySystemWebAPI.Models;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace CompanySystemWebAPI.Services
{
    public class DepartementsService : IDepartementsService
    {
        private readonly CompanyContext _companyContext;

        public DepartementsService(CompanyContext companyContext)
        {
            _companyContext = companyContext;
        }
        public async Task<Departement> Create(DepartementsAddDto dept)
        {

            var d = _companyContext.Departements.Any(x=>x.Deptname == dept.Deptname);
            if (d)
            {
                return null;
            }
            
            var newDept = new Departement()
            {
                Deptname = dept.Deptname,
                Mgrempno = dept.Mgrempno,
            };

            if(dept.Mgrempno == null)
            {
                newDept.Mgrempno = null;
            }
            _companyContext.Departements.Add(newDept);
            await _companyContext.SaveChangesAsync();

            return newDept;
        }

        public async Task<Departement> Delete(int id)
        {
            var d = await _companyContext.Departements.FindAsync(id);
            if (d == null)
            {
                return null;
            }

            _companyContext.Departements.Remove(d);
            await _companyContext.SaveChangesAsync();

            return d;
        }

        public async Task<Departement> GetDepartement(int id)
        {
            var dept = await _companyContext.Departements.FindAsync(id);
            if (dept == null)
            {
                return null;
            }
            return dept;
        }

        public async Task<List<Departement>> GetDepartements(int pageNumber, int perPage)
        {
            return await _companyContext.Departements
                .OrderBy(ob => ob.Deptno)
                .Skip((pageNumber - 1) * perPage)
                .Take(perPage)
                .ToListAsync<Departement>();
        }

        public async Task<Departement> Update(int id, DepartementsAddDto dept)
        {
            var d = await _companyContext.Departements.FindAsync(id);
            if (d == null)
            {
                return null;
            }
            d.Deptname = dept.Deptname;
            d.Mgrempno = dept.Mgrempno;

            _companyContext.Departements.Update(d);
            await _companyContext.SaveChangesAsync();

            return d;
        }

    }
}

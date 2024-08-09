using CompanySystemWebAPI.Dtos.WorksOnDto;
using CompanySystemWebAPI.Interfaces;
using CompanySystemWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CompanySystemWebAPI.Services
{
    public class WorksOnService : IWorksOnService
    {
        private readonly CompanyContext _companyContext;

        public WorksOnService(CompanyContext companyContext)
        {
            _companyContext = companyContext;
        }
        public async Task<Workson> Create(WorksOnAddDto wo)
        {

            var newWo = new Workson()
            {
                Empno = wo.Empno,
                Projno = wo.Projno,
                Dateworked = wo.Dateworked,
                Hoursworked = wo.Hoursworked,
            };

            _companyContext.Worksons.Add(newWo);
            await _companyContext.SaveChangesAsync();

            return newWo;
        }

        public async Task<Workson> Delete(int projNo, int empNo)
        {

            var wo = await (from value in _companyContext.Worksons
                            where (value.Projno == projNo && value.Empno == empNo)
                            select value).FirstOrDefaultAsync();
            if (wo == null)
            {
                return null;
            }

            _companyContext.Worksons.Remove(wo);
            await _companyContext.SaveChangesAsync();

            return wo;
        }

        public async Task<Workson> GetWorkson(int projNo, int empNo)
        {

            var wo = await (from value in _companyContext.Worksons
                            where (value.Projno == projNo && value.Empno == empNo)
                            select value).FirstOrDefaultAsync();
                 
            if (wo == null)
            {
                return null;
            }
            return wo;
        }

        public async Task<List<Workson>> GetWorksons(int pageNumber, int perPage)
        {
            return await _companyContext.Worksons
            .OrderBy(ob => ob.Empno)
            .Skip((pageNumber - 1) * perPage)
            .Take(perPage)
            .ToListAsync<Workson>();
        }

        public async Task<Workson> Update(int projNo, int empNo, WorksOnUpdateDto wo)
        {

            var w = await (from value in _companyContext.Worksons
                            where (value.Projno == projNo && value.Empno == empNo)
                            select value).FirstOrDefaultAsync();

            if (w == null)
            {
                return null;
            }

            w.Dateworked = wo.Dateworked;
            w.Hoursworked = wo.Hoursworked;

            _companyContext.Worksons.Update(w);
            await _companyContext.SaveChangesAsync();

            return w;
        }

        public async Task<List<object>> GetTotalHoursWorkedFemale()
        {
            var total = from emp in _companyContext.Employees
                        join work in _companyContext.Worksons on emp.Empno equals work.Empno
                        where (emp.Sex == "Female")
                        orderby (emp.Lname)
                        group work by emp.Deptno;

            var result = await total
                         .Select(s => new
                         {
                             DeptNo = s.Key,
                             DeptName = _companyContext.Departements
                             .Where(x=> x.Deptno == s.Key)
                             .Select(s=>s.Deptname)
                             .FirstOrDefault(),
                             Employee = s
                             .GroupBy(gb => gb.Empno)
                             .Select(e => new
                             {
                                 Name = e
                                 .Select(n=>n.EmpnoNavigation.Fname +" "+ n.EmpnoNavigation.Lname)
                                 .Distinct()
                                 .FirstOrDefault(),
                                 TotalHours = e
                                 .Select(th => th.Hoursworked).Sum(),
                             }),
                         })
                         .ToListAsync<object>();

            return result;
        }

        public async Task<List<object>> GetTotalHoursWorked()
        {
            var result = await (from emp in _companyContext.Employees
                                join work in _companyContext.Worksons on emp.Empno equals work.Empno
                                group work by work.Empno)
                        .Select(s => new
                        {
                            Name = s
                            .Select(n => n.EmpnoNavigation.Fname + " "+ n.EmpnoNavigation.Lname)
                            .FirstOrDefault(),
                            TotalHours = s
                            .Select(th => th.Hoursworked).Sum(),
                        })
                        .OrderBy(ob => ob.Name)
                        .ToListAsync<object>();
                        

            return result;
        }
    }
}

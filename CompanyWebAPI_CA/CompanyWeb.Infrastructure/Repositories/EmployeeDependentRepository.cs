using CompanyWeb.Domain.Models.Entities;
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
    public class EmployeeDependentRepository : IEmployeeDependentRepository
    {
        private readonly CompanyDbContext Context;
        public EmployeeDependentRepository(CompanyDbContext context)
        {
            Context = context;
        }
        public async Task<EmployeeDependent> Create(EmployeeDependent employeeDependent)
        {
            Context.EmployeeDependents.Add(employeeDependent);
            await Context.SaveChangesAsync();
            return employeeDependent;
        }

        public async Task<List<EmployeeDependent>> Delete(int empNo)
        {
            var dependents = Context.EmployeeDependents.Where(w => w.DependentEmpno == empNo);
            Context.EmployeeDependents.RemoveRange(dependents);
            await Context.SaveChangesAsync();
            return dependents.ToList();
        }

        public async Task<IQueryable<EmployeeDependent>> GetAllEmployeeDependents()
        {
            return Context.EmployeeDependents;
        }

        public async Task<List<EmployeeDependent>> GetEmployeeDependentByEmpNo(int empNo)
        { 
            var dependents = await Context.EmployeeDependents.Where(w => w.DependentEmpno == empNo).ToListAsync();
            return dependents;
        }


    }
}

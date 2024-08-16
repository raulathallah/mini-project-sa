using CompanyWeb.Application.Repositories;
using CompanyWeb.Domain.Models.Dtos;
using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Requests;
using CompanyWeb.Domain.Models.Responses;
using LMS.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly CompanyDbContext Context;
        public EmployeeRepository(CompanyDbContext context)
        {
            Context = context;
        }
        public async Task<EmployeeDetailDto> Create(AddEmployeeRequest request)
        {
            var newEmp = new Employee()
            {
                Fname = request.Fname,
                Lname = request.Lname,
                Dob = request.Dob,
                Sex = request.Sex,
                Address = request.Address,
                Position = request.Position,
                Deptno = request.Deptno,
            };

            //add to db
            Context.Employees.Add(newEmp);
            await Context.SaveChangesAsync(); 

            return new EmployeeDetailDto()
            {
                Empno = newEmp.Empno,
                Fname = request.Fname,
                Lname = request.Lname,
                Dob = request.Dob,
                Sex = request.Sex,
                Address = request.Address,
                Position = request.Position,
                Deptno = request.Deptno,
            };

        }
        public async Task<Employee> Delete(int id)
        {
            var e = await Context.Employees.FindAsync(id);
            if (e == null)
            {
                return null;
            }

            Context.Employees.Remove(e);
            await Context.SaveChangesAsync();

            return e;
        }
        public async Task<Employee> GetEmployee(int id)
        {
            var emp = await Context.Employees.FindAsync(id);
            if (emp == null)
            {
                return null;
            }
            return emp;
        }
        public async Task<List<Employee>> GetEmployees(int pageNumber, int perPage)
        {
            return await Context.Employees
                .OrderBy(ob => ob.Empno)
                .Skip((pageNumber - 1) * perPage)
                .Take(perPage)
                .ToListAsync<Employee>();
        }
        public async Task<IQueryable<Employee>> GetAllEmployees()
        {
            return Context.Employees;
            
        }
        public async Task<Employee> Update(int id, UpdateEmployeeRequest request)
        {
            var e = await Context.Employees.FindAsync(id);
            if (e == null)
            {
                return null;
            }
            e.Deptno = request.Deptno;
            e.Address = request.Address;
            e.Position = request.Position;
            e.Dob = request.Dob;
            e.Fname = request.Fname;
            e.Lname = request.Lname;
            e.Sex = request.Sex;

            Context.Employees.Update(e);
            await Context.SaveChangesAsync();

            return e;
        }

    }
}

using CompanySystemWebAPI.Dtos.EmployeesAddDto;
using CompanySystemWebAPI.Interfaces;
using CompanySystemWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;

namespace CompanySystemWebAPI.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly CompanyContext _companyContext;

        public EmployeesService(CompanyContext companyContext)
        {
            _companyContext = companyContext;
        }

        public async Task<Employee> Create(EmployeesAddDto emp)
        {
            var newEmp = new Employee()
            {
                Fname = emp.Fname,
                Lname = emp.Lname,
                Dob = emp.Dob,
                Sex = emp.Sex,
                Address = emp.Address,
                Position = emp.Position,
                Deptno = emp.Deptno,
            };

            //add new emp
            _companyContext.Employees.Add(newEmp);

            await _companyContext.SaveChangesAsync();
            return newEmp;
        } 
        public async Task<Employee> Delete(int id)
        {
            var e = await _companyContext.Employees.FindAsync(id);
            if (e == null)
            {
                return null;
            }

            _companyContext.Employees.Remove(e);
            await _companyContext.SaveChangesAsync();

            return e;
        }

        public async Task<Employee> GetEmployee(int id)
        {
            var emp = await _companyContext.Employees.FindAsync(id);
            if(emp == null)
            {
                return null;
            }
            return emp;
        }

        public async Task<List<Employee>> GetEmployees(int pageNumber, int perPage)
        {
            return await _companyContext.Employees
                .OrderBy(ob=>ob.Empno)
                .Skip((pageNumber - 1) * perPage)
                .Take(perPage)
                .ToListAsync<Employee>();
        }
        public async Task<Employee> Update(int id, EmployeesAddDto emp)
        {
            var e = await _companyContext.Employees.FindAsync(id);
            if (e == null)
            {
                return null;
            }
            e.Deptno = emp.Deptno;
            e.Address = emp.Address;
            e.Position = emp.Position;
            e.Dob = emp.Dob;
            e.Fname = emp.Fname;
            e.Lname = emp.Lname;
            e.Sex = emp.Sex;

            _companyContext.Employees.Update(e);
            await _companyContext.SaveChangesAsync();

            return e;
        }

        public async Task<List<object>> GetEmployeeBetweenEightyAndNinety()
        {
            var emp = await _companyContext.Employees
                .Where(x => x.Dob.Year > 1980 && x.Dob.Year < 1990)
                .ToListAsync<object>();

            return emp;
        }

        public async Task<List<object>> GetEmployeeFemaleAfterNinety()
        {
            var emp = await _companyContext.Employees
                .Where(x => x.Sex == "Female" && x.Dob.Year > 1990)
                .ToListAsync<object>();

            return emp;
        }

        public async Task<List<object>> GetEmployeeFemaleManagerInOrder()
        {
            var emp = await (from value in _companyContext.Employees
                             join dept in _companyContext.Departements on value.Deptno equals dept.Deptno
                             where value.Sex == "Female" && dept.Mgrempno == value.Empno
                             select value)
                             .ToListAsync<object>();
            return emp;
        }

        public async Task<List<object>> GetEmployeeNotManager()
        {
            var emp = from value in _companyContext.Employees
                      join dept in _companyContext.Departements on value.Empno equals dept.Mgrempno
                      select value;

            var result = await _companyContext.Employees
                .Except(emp)
                .ToListAsync<object>(); 
            return result;
        }

        public async Task<List<object>> GetEmployeeIT()
        {
            var deptId = _companyContext.Departements.SingleOrDefault(s => s.Deptname == "IT")?.Deptno;

            if(deptId == null)
            {
                return null;
            }

            var result = await _companyContext.Employees
                .Where(w => w.Deptno == deptId)
                .Select(s => new
                {
                    Name = s.Fname + " " + s.Lname,
                    Departement = s.DeptnoNavigation.Deptname,
                    Address = s.Address,
                })
                .ToListAsync<object>();
            return result;
        }

        public async Task<List<object>> GetEmployeeBRICS()
        {
            string[] brics = { "Brazil", "Russia", "India", "China", "South Africa" };
            var result = await _companyContext.Employees
                .Where(w => brics.Any(any=> w.Address.Contains(any)))
                .Select(s => new
                {
                    Name = s.Fname + " " + s.Lname,
                    Address = s.Address,
                })
                .ToListAsync<object>();
            return result;
        }
        public async Task<List<object>> GetManagerUnderFourty()
        {
            var emp = from value in _companyContext.Employees
                      join dept in _companyContext.Departements on value.Empno equals dept.Mgrempno
                      select value;

            var result = await emp
                .Where(w => DateOnly.FromDateTime(DateTime.Now).Year - w.Dob.Year < 40)
                .ToListAsync<object>();
            return result;
        }

        public async Task<int> GetManagerFemaleCount()
        {
            var emp = await (from value in _companyContext.Employees
                      join dept in _companyContext.Departements on value.Empno equals dept.Mgrempno
                      where value.Sex == "Female"
                      select value).CountAsync();

  /*          var result = await emp
                .Where(w => w.Sex == "Female")
                .CountAsync();*/

            return emp;
        }

        public async Task<List<object>> GetEmployeeRetireThisYear()
        {
            var emp = await _companyContext.Employees
                .Where(w => DateOnly.FromDateTime(DateTime.Now).Year - w.Dob.Year >= 45)
                .Select(s => new
                {
                    Nama = s.Fname,
                    Age = DateOnly.FromDateTime(DateTime.Now).Year - s.Dob.Year
                })
                .ToListAsync<object>();      
            return emp;
        }

        public async Task<List<object>> GetEmployeeAges()
        {
            var emp = await _companyContext.Employees
                .Select(s=>new
                {
                    Name = s.Fname + " " + s.Lname,
                    Departement = s.DeptnoNavigation.Deptname,
                    Age = DateOnly.FromDateTime(DateTime.Now).Year - s.Dob.Year,
                })
                .ToListAsync<object>();

            return emp;
        }

        public async Task<List<object>> GetEmployeeNotManagerAndSupervisor()
        {
            var emp = from value in _companyContext.Employees
                      join dept in _companyContext.Departements on value.Empno equals dept.Mgrempno
                      select value;

            var result = await _companyContext.Employees
                .Except(emp)
                .Where(w=> !w.Position.Contains("Supervisor"))
                .Select(s=> new
                {
                    FirstName = s.Fname,
                    LastName = s.Lname,
                    Position = s.Position,
                    Sex = s.Sex,
                    Deptno = s.Deptno,
                })
                .ToListAsync<object>();
            return result;
        }
        


    }
}

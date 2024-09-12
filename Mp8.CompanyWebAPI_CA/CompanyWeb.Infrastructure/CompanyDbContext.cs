using CompanyWeb.Domain.Models.Auth;
using CompanyWeb.Domain.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Infrastructure
{
    public partial class CompanyDbContext : IdentityDbContext<AppUser>
    {
        public CompanyDbContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Departement> Departements { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Workson> Worksons { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<DepartementLocation> DepartementLocations { get; set; }
        public virtual DbSet<EmployeeDependent> EmployeeDependents { get; set; }
        public virtual DbSet<Workflow> Workflows { get; set; }
        public virtual DbSet<WorkflowAction> WorkflowActions { get; set; }
        public virtual DbSet<WorkflowSequence> WorkflowSequences { get; set; }
        public virtual DbSet<Process> Process { get; set; }
        public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }
        public virtual DbSet<NextStepRules> NextStepRules { get; set; }
    }
}

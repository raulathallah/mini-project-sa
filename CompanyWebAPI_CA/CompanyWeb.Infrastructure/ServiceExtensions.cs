using CompanyWeb.Application.Repositories;
using CompanyWeb.Application.Services;
using CompanyWeb.Infrastructure.Repositories;
using CompanyWeb.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("CompanyContext");
            services.AddDbContext<CompanyDbContext>(options => options.UseNpgsql(connection));
            // repository //
            services.AddScoped<IDepartementRepository, DepartementRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IWorksOnRepository, WorksOnRepository>();

            // service //
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IWorksOnService, WorksOnService>();
            services.AddScoped<IDepartementService, DepartementService>();
        }
    }
}

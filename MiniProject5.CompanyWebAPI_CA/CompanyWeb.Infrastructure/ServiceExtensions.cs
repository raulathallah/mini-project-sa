
using CompanyWeb.Application.Services;
using CompanyWeb.Domain.Repositories;
using CompanyWeb.Domain.Services;
using CompanyWeb.Infrastructure.Repositories;
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
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            // repository //
            services.AddScoped<IDepartementRepository, DepartementRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IWorksOnRepository, WorksOnRepository>();
            services.AddScoped<IDepartementLocationRepository, DepartementLocationRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IEmployeeDependentRepository, EmployeeDependentRepository>();

            // service //
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IWorksOnService, WorksOnService>();
            services.AddScoped<IDepartementService, DepartementService>();
            services.AddScoped<IDepartementLocationService, DepartementLocationService>();


            services.AddCors();
        }
    }
}

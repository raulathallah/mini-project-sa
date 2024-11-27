
using CompanyWeb.Application.Services;
using CompanyWeb.Domain.Models.Auth;
using CompanyWeb.Domain.Repositories;
using CompanyWeb.Domain.Services;
using CompanyWeb.Infrastructure;
using CompanyWeb.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IWorkflowRepository, WorkflowRepository>();

            // service //
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IWorksOnService, WorksOnService>();
            services.AddScoped<IDepartementService, DepartementService>();
            services.AddScoped<IDepartementLocationService, DepartementLocationService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IPdfService, PdfService>();

            // Identity //
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<CompanyDbContext>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                options.DefaultChallengeScheme =
                options.DefaultForbidScheme =
                options.DefaultScheme =
                options.DefaultSignInScheme =
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(configuration["JWT:Key"])
                    )
                }; 
                
                options.Events = new JwtBearerEvents // Handler untuk menyimpan token di cookie
                {
                    OnTokenValidated = context =>
                    {
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        context.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    },
                    OnMessageReceived = context =>

                    {
                        context.Token = context.Request.Cookies["AuthToken"];
                        return Task.CompletedTask;
                    }

                };
            });
        }
    }
}

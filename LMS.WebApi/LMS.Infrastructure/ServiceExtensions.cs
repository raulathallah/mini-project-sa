using LMS.Application.IRepositories;
using LMS.Core.IRepositories;
using LMS.Core.Services;
using LMS.Infrastructure.Manager;
using LMS.Infrastructure.Repository;
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
            var connection = configuration.GetConnectionString("LMSConnection");
            services.AddDbContext<LMSDbContext>(options => options.UseNpgsql(connection));
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookusertransactionRepository, BookusertransactionRepository>();
            services.AddScoped<IBookManager, BookManager>();
        }
    }
}

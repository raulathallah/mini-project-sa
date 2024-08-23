using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Infrastructure.Context
{
    public class LMSDbContextFactory : IDesignTimeDbContextFactory<LMSDbContext>
    {
        public LMSDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LMSDbContext>();
            optionsBuilder.UseNpgsql("Server=localhost;Database=lms_asg5;Username=postgres;Password=password");

            return new LMSDbContext(optionsBuilder.Options);
        }
    }
}

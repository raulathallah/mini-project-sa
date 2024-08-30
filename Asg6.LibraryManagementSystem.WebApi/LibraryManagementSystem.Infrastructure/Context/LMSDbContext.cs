using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.Domain.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Infrastructure.Context
{
    public class LMSDbContext : IdentityDbContext<AppUser>
    {
        public LMSDbContext(DbContextOptions<LMSDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<BookUserTransactions> BookUserTransactions { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}

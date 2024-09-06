using CompanyWeb.Domain.Repositories;
using LMS.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly CompanyDbContext Context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleRepository(CompanyDbContext context, RoleManager<IdentityRole> roleManager)
        {
            Context = context;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<IdentityRole>> GetAllRole()
        {
            var roleStore = new RoleStore<IdentityRole>(Context);
            return roleStore.Roles;
        }
    }
}

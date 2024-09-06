using CompanyWeb.Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<IdentityRole>> GetAllRole();
    }
}

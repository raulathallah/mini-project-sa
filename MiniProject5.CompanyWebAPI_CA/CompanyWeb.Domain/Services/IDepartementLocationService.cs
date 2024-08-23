using CompanyWeb.Domain.Models.Entities;
using CompanyWeb.Domain.Models.Requests.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CompanyWeb.Domain.Services
{
    public interface IDepartementLocationService
    {
        Task<DepartementLocation> Create(AddDepartementLocationRequest request);
    }
}

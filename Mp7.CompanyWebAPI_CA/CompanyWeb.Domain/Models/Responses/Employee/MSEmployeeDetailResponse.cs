using CompanyWeb.Domain.Models.Dtos;
using CompanyWeb.Domain.Models.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Responses.Employee
{
    public class MSEmployeeDetailResponse : BaseResponse
    {
        public EmployeeResponse Data { get; set; }
    }
}

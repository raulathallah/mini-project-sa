using CompanyWeb.Domain.Models.Dtos;
using CompanyWeb.Domain.Models.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Responses
{
    public class WorksOnDetailResponse : BaseResponse
    {
        public WorksOnDetailDto Data { get; set; }
    }
}

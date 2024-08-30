using CompanyWeb.Domain.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyWeb.Domain.Models.Responses.Base
{
    public class ProjectDetailResponse : BaseResponse
    {
        public ProjectResponse Data { get; set; }
    }
}

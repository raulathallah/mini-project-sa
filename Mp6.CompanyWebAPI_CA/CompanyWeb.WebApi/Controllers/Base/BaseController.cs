using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace CompanyWeb.WebApi.Controllers.Base
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }
}

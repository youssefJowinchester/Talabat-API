using Microsoft.AspNetCore.Mvc;
using Talabat_.APIS.Errors;

namespace Talabat_.APIS.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        public ActionResult Error(int code)
        {
            return NotFound(new ApiResponse(code, "Endpoint is not found!"));
        }
    }
}

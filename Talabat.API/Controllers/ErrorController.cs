using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Errors;

namespace Talabat.API.Controllers
{
    [ApiController]
    [Route("/Errors/{code}")]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorController : ControllerBase
    {
        public IActionResult NotFoundEndPoint()
        {
            return NotFound(new ApiResponse(404));
        }







    }
}

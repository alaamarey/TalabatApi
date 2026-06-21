using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Errors;

namespace Talabat.API.Controllers
{
    public class BuggyController : BaseController
    {


        [HttpGet("NotFound")]
        // 1. NotFound Error 404
        public IActionResult NotFoundError()
        {
            return NotFound(new ApiResponse(404));
        }





        [HttpGet("BadRequest")]
        // 2. BadRequest Error 400
        public IActionResult BadRequestError()
        {
            return BadRequest( new ApiResponse(400));
        }



        [HttpGet("Validation/{id}")]
        // 3. Validation Error  400 
        public ActionResult ValidationError(int id)
        {
            return Ok();
        }





        [HttpGet("Exception")]
        // 4. Server Error (Exception) 500
        public ContentResult ServerError()
        {
            string? msg = null;
            return Content(msg!.Length.ToString());
        }








    }
}

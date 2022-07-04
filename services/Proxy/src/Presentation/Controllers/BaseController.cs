using Application.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.Response;

namespace Presentation.Controllers
{
    public class BaseController : ControllerBase
    {
        [NonAction]
        public ActionResult BadRequestResponse(Error error)
        {
            var genericError = new GenericErrorPayload
            {
                Status = StatusCodes.Status400BadRequest.ToString(),
                Code = error.ErrorCode,
                Message = error.Message
            };

            return BadRequest(genericError);
        }


        [NonAction]
        public ActionResult NotFoundResponse(Error error)
        {
            var genericError = new GenericErrorPayload
            {
                Status = StatusCodes.Status404NotFound.ToString(),
                Code = error.ErrorCode,
                Message = error.Message
            };

            return NotFound(genericError);
        }
    }
}

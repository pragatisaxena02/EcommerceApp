using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;

namespace Products.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController : ControllerBase
    {

        [HttpGet("/notfound")]
        public IActionResult ReturnsNotFound()
        {
            return NotFound();
        }

        [HttpGet("/bad-request")]
        public IActionResult ReturnsBadRequest()
        {
            return BadRequest();
        }

        [HttpGet("/un-authorized")]
        public IActionResult ReturnsUnAuthorized()
        {
            return Unauthorized();
        }

        [HttpPost("/validation-error")]
        public IActionResult GetValidationError(Product.Domain.Product product)
        {
            return Ok();
        }

        [HttpGet("/internal-server-error")]
        public IActionResult ReturnsInternalServerError()
        {
            try
            {
                // Simulate an exception
                throw new Exception("Internal Server Error occurred");
            }
            catch (Exception ex)
            {
                var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

                // Log the exception or handle it as needed
                // For demonstration purposes, we'll just return the exception message and stack trace
                return StatusCode(500, $"Internal Server Error: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}");
            }
        }
    }
}

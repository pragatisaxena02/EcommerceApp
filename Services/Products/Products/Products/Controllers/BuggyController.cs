using Microsoft.AspNetCore.Mvc;
using Product.Domain.Query;
using Product.Domain;
using System.Net;

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

        [HttpGet("/internal-server-error")]
        public IActionResult ReturnsInternalServerError()
        {
            return StatusCode(500, "Internal Server Error");
        }
    }
}

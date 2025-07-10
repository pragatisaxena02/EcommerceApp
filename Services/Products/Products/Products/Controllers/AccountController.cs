using Infrastructure.DbEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Dtos;
using System.Security.Claims;

namespace Products.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register( RegisterDto registerDto)
        {
            var user = new AppUser
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName
            };

            var result = await _signInManager.UserManager.CreateAsync(user, registerDto.Password);
            if( !result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem(ModelState);
            }

            return Ok();
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return NoContent();
        }

        [HttpPost("user-info")]
        public async Task<ActionResult> GetUserInfo()
        {
            if (User.Identity?.IsAuthenticated == false)
                return NoContent();

            var user = await _signInManager.UserManager.Users.FirstOrDefaultAsync( x => x.Email == User.FindFirstValue( ClaimTypes.Email) );
            if (user == null)
                return Unauthorized();

            return Ok(new
            {
               user.FirstName,
               user.LastName,
               user.Email
            });
        }

        [HttpGet]
        public ActionResult GetAuthState()
        {
           return Ok(new
           {
               IsAuthenticated = User.Identity?.IsAuthenticated ?? false,
               UserName = User.Identity?.Name,
               Email = User.FindFirstValue(ClaimTypes.Email)
           });
        }
    }
}

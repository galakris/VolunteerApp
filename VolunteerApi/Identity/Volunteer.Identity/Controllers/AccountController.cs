using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Volunteer.Identity.DAL.Entities;
using Volunteer.Identity.Models;

namespace Volunteer.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new AppUser { UserName = model.UserName, Email = model.Email, RegistrationDate = DateTime.UtcNow };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("userName", user.UserName));
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("email", user.Email));
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("role", "normal_user"));

            return Ok(new
            {
                Email = model.Email,
                UserName = model.UserName,
                RegistrationDate = user.RegistrationDate
            });
        }
    }
}
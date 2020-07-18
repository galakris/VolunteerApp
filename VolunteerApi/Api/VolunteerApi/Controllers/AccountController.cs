using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volunteer.Services.Users.Interfaces;
using Volunteer.Services.Users.Models;
using VolunteerApi.Validators;

namespace VolunteerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(typeof(UserAccountDto), 200)]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto model)
        {        
            var validator = new RegisterRequestValidator();
            await validator.ValidateAndThrowAsync(model);
            return Ok(await _userService.Create(model));
        }
    }
}

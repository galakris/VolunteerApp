using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volunteer.Services.Needs.Models;
using Volunteer.Services.Volunteers.Interfaces;

namespace VolunteerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolunteersController : ControllerBase
    {
        private readonly IVolunteerService _volunteerService;

        public VolunteersController(IVolunteerService volunteerService)
        {
            _volunteerService = volunteerService;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(AssignVolunteerToNeedResponseDto), 200)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _volunteerService.SearchVolunteers());
        }
    }
}
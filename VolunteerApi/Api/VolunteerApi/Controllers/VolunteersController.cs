using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volunteer.Services.Needs.Models;
using Volunteer.Services.Volunteers.Interfaces;
using Volunteer.Services.Volunteers.Models;

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
        [ProducesResponseType(typeof(List<SearchVolunteersResponseDto>), 200)]
        public async Task<IActionResult> Get([FromQuery] int maxDistanceKm)
        {
            return Ok(await _volunteerService.SearchVolunteers(maxDistanceKm));
        }
    }
}
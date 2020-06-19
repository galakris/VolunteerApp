using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volunteer.Services.Needs.Interfaces;
using Volunteer.Services.Needs.Models;

namespace VolunteerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NeedsController : ControllerBase
    {
        private readonly INeedService _needService;

        public NeedsController(INeedService needService)
        {
            _needService = needService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _needService.GetNeeds());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateNeedRequestDto requestDto)
        {
            return Ok(await _needService.CreateNeed(requestDto));
        }

        [Authorize]
        [HttpPost("{needId}/takeExecution")]
        public async Task<IActionResult> TakeExecution([FromBody] CreateNeedRequestDto requestDto)
        {
            return Ok(await _needService.CreateNeed(requestDto));
        }
    }
}

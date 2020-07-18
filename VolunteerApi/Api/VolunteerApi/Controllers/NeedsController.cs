using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volunteer.DAL.Enums;
using Volunteer.Services.Needs.Interfaces;
using Volunteer.Services.Needs.Models;
using Volunteer.SharedObjects.Models;
using VolunteerApi.Validators;

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
        [ProducesResponseType(typeof(ICollection<NeedDto>), 200)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _needService.GetNeeds());
        }

        [Authorize]
        [HttpGet("my")]
        [ProducesResponseType(typeof(ICollection<NeedDto>), 200)]
        public async Task<IActionResult> GetMy()
        {
            return Ok(await _needService.GetNeeds(true));
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(CreateNeedResponseDto), 200)]
        public async Task<IActionResult> Create([FromBody] CreateNeedRequestDto requestDto)
        {
            var validator = new CreateNeedRequestDtoValidator();
            validator.ValidateAndThrow(requestDto);
            return Ok(await _needService.CreateNeed(requestDto));
        }

        [Authorize]
        [HttpGet("{needId}/takeExecution")]
        [ProducesResponseType(typeof(AssignVolunteerToNeedResponseDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> TakeExecution([FromRoute] int needId)
        {
            return Ok(await _needService.AssignVolunteerToNeed(needId));
        }

        [Authorize]
        [HttpGet("{needId}/finish")]
        [ProducesResponseType(typeof(AssignVolunteerToNeedResponseDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> End([FromRoute] int needId)
        {
            return Ok(await _needService.ModifyNeedStatus(needId, NeedStatus.Finished));
        }

        [Authorize]
        [HttpDelete("{needId}")]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> DeleteNeed([FromRoute] int needId)
        {
            return Ok(await _needService.DeleteNeed(needId));
        }
    }
}

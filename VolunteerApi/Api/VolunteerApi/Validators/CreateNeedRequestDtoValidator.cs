using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Volunteer.Services.Needs.Models;

namespace VolunteerApi.Validators
{
    public class CreateNeedRequestDtoValidator : AbstractValidator<CreateNeedRequestDto>
    {
        public CreateNeedRequestDtoValidator()
        {
            RuleFor(x => x.DeadlineDate).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Latitude).NotEmpty();
            RuleFor(x => x.Longitude).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}

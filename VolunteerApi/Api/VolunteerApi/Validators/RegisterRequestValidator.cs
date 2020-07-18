using FluentValidation;
using Volunteer.Services.Users.Models;

namespace VolunteerApi.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequestDto>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Longitude).NotEmpty();
            RuleFor(x => x.Latitude).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Telephone).NotEmpty();
        }
    }
}
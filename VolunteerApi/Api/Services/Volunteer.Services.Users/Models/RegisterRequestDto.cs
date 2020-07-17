using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volunteer.DAL.Enums;

namespace Volunteer.Services.Users.Models
{
    public class RegisterRequestDto
    {

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Role Role { get; set; }

        public string Telephone { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}

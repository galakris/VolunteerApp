using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Volunteer.Identity.DAL.Entities
{
    public class AppUser : IdentityUser
    {
        public DateTime RegistrationDate { get; set; }
    }
}

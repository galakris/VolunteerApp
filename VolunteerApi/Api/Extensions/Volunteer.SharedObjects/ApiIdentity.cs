using System;
using System.Security.Claims;
using Volunteer.DAL.Enums;

namespace Volunteer.SharedObjects
{
    public class ApiIdentity : ClaimsIdentity
    {
        public int UserAcountId { get; set; }

        public Role Role { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}

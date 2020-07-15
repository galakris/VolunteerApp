using System;
using System.Security.Claims;
using Volunteer.DAL.Enums;

namespace Volunteer.SharedObjects
{
    public class ApiIdentity : ClaimsIdentity
    {
        public int UserAcountId { get; set; }

        public Role Role { get; set; }
    }
}

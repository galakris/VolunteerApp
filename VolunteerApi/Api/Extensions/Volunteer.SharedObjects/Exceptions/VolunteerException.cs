using System;
using System.Collections.Generic;
using System.Text;
using Volunteer.SharedObjects.Models;

namespace Volunteer.SharedObjects.Exceptions
{
    public class VolunteerException : Exception
    {
        public VolunteerException(string details)
        {
            ErrorDetails = new ErrorResponse(details);
        }

        public ErrorResponse ErrorDetails { get; private set; }
    }
}

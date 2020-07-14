using System;
using System.Collections.Generic;
using System.Text;

namespace Volunteer.SharedObjects.Models
{
    public class ErrorResponse
    {
        public ErrorResponse(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; private set; }
    }
}

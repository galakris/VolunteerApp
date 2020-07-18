using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;
using Volunteer.DAL.Enums;

namespace Volunteer.Services.Auth.Models
{
    public class LoginResponseDto
    {
        public string Token { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Role Role { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
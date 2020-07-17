using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;
using Volunteer.DAL.Enums;

namespace Volunteer.Services.Needs.Models
{
    public class NeedDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public NeedCategory Category { get; set; }

        public string Description { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Distance { get; set; }

        public NeedUser AssignedVolunteer { get; set; }

        public NeedUser Needy { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public NeedStatus NeedStatus { get; set; }

        public DateTime DeadlineDate { get; set; }
    }

    public class NeedUser
    {
        public string FirstName { get; set; }

        public string Telephone { get; set; }
    }
}

using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;
using Volunteer.DAL.Enums;

namespace Volunteer.Services.Needs.Models
{
    public class CreateNeedResponseDto
    {
        public int NeedId { get; set; }

        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public NeedCategory Category { get; set; }

        public string Description { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public NeedStatus NeedStatus { get; set; }

        public DateTime DeadlineDate { get; set; }
    }
}
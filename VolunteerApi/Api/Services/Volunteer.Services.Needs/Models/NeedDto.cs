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
        public int NeedId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public NeedCategory Category { get; set; }

        public string Description { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int Distance { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public NeedStatus NeedStatus { get; set; }

        public DateTime DeadlineDate { get; set; }
    }
}

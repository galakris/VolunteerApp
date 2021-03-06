﻿using System;
using System.Collections.Generic;
using System.Text;
using Volunteer.DAL.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Volunteer.Services.Users.Models
{
    public class UserAccountDto
    {
        public string UserName { get; set; }

        public int UserAccountId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Role Role { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}

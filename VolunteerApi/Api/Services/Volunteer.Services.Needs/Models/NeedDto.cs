﻿using System;
using System.Collections.Generic;
using System.Text;
using Volunteer.DAL.Enums;

namespace Volunteer.Services.Needs.Models
{
    public class NeedDto
    {
        public int NeedId { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public NeedStatus NeedStatus { get; set; }

        public DateTime DeadlineDate { get; set; }
    }
}

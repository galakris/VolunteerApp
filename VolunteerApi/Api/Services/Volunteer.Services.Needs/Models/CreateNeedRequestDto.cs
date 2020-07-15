using System;
using System.Collections.Generic;
using System.Text;

namespace Volunteer.Services.Needs.Models
{
    public class CreateNeedRequestDto
    {
        public string Description { get; set; }

        public string Name { get; set; }

        public DateTime DeadlineDate { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}

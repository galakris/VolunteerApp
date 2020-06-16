using System;
using System.Collections.Generic;
using System.Text;

namespace Volunteer.Services.Needs.Models
{
    public class NeedDto
    {
        public int NeedId { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public DateTime DeadlineDate { get; set; }
    }
}

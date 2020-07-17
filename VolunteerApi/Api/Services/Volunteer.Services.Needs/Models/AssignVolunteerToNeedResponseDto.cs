using System;
using System.Collections.Generic;
using System.Text;

namespace Volunteer.Services.Needs.Models
{
    public class AssignVolunteerToNeedResponseDto
    {
        public int VolunteerUserAccountId { get; set; }

        public int NeedId { get; set; }

        public string NeedyFirstName { get; set; }

        public string NeedyTelephone { get; set; }
    }
}

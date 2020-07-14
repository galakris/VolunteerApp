using System;
using System.Collections.Generic;
using System.Text;

namespace Volunteer.Services.Needs.Models
{
    public class AssignVolunteerToNeedResponseDto
    {
        public int UserAccountId { get; set; }

        public int NeedId { get; set; }
    }
}

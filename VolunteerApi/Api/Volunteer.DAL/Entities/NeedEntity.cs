using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Volunteer.DAL.Entities.Base;
using Volunteer.DAL.Enums;
using Volunteer.DAL.Relations;

namespace Volunteer.DAL.Entities
{
    public class NeedEntity : Entity
    {
        public string Name { get; set; }

        public NeedCategory Category { get; set; }

        public string Description { get; set; }

        public DateTime DeadlineDate { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public NeedStatus NeedStatus { get; set; }

        public virtual ICollection<UserAccountNeed> UserAccountNeeds { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Volunteer.DAL.Entities.Base;
using Volunteer.DAL.Relations;

namespace Volunteer.DAL.Entities
{
    public class NeedEntity : Entity
    {
        public string Category { get; set; }

        public string Description { get; set; }

        public DateTime DeadlineDate { get; set; }

        public virtual ICollection<UserAccountNeed> UserAccountNeeds { get; set; }
    }
}

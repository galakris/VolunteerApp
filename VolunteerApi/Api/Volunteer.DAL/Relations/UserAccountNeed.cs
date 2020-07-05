using System;
using System.Collections.Generic;
using System.Text;
using Volunteer.DAL.Entities;
using Volunteer.DAL.Enums;

namespace Volunteer.DAL.Relations
{
    public class UserAccountNeed
    {
        public int UserAccountId { get; set; }

        public int NeedId { get; set; }

        public Role Role { get; set; }

        public virtual NeedEntity Need { get; set; }
        public virtual UserAccountEntity UserAccount { get; set; }
    }
}

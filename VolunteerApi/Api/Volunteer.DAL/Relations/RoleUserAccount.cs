using System;
using System.Collections.Generic;
using System.Text;
using Volunteer.DAL.Entities;

namespace Volunteer.DAL.Relations
{
    public class RoleUserAccount
    {
        public int UserAccountId { get; set; }

        public int RoleId { get; set; }

        public virtual UserAccountEntity UserAccount { get; set; }

        public virtual RoleEntity Role { get; set; }
    }
}

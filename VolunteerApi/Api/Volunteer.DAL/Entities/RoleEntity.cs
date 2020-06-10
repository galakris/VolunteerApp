using System;
using System.Collections.Generic;
using System.Text;
using Volunteer.DAL.Entities.Base;
using Volunteer.DAL.Relations;

namespace Volunteer.DAL.Entities
{
    public class RoleEntity : Entity
    {
        public string RoleName { get; set; }

        public string RoleDetails { get; set; }

        public virtual ICollection<RoleUserAccount> RoleUserAccount { get; set; }
    }
}

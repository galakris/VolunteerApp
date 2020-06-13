using System.Collections.Generic;
using Volunteer.DAL.Entities.Base;
using Volunteer.DAL.Relations;

namespace Volunteer.DAL.Entities
{
    public class UserAccountEntity : Entity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public virtual ICollection<RoleUserAccount> RoleUserAccount { get; set; }

        public virtual ICollection<UserAccountNeed> UserAccountNeeds { get; set; }
    }
}

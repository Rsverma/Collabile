using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Collabile.Api.Models
{
    public class CollabileRole : IdentityRole
    {
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public virtual ICollection<CollabileRoleClaim> RoleClaims { get; set; }

        public CollabileRole() : base()
        {
            RoleClaims = new HashSet<CollabileRoleClaim>();
        }

        public CollabileRole(string roleName, string roleDescription = null) : base(roleName)
        {
            RoleClaims = new HashSet<CollabileRoleClaim>();
            Description = roleDescription;
        }
    }
}

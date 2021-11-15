using System;
using Microsoft.AspNetCore.Identity;

namespace Collabile.Api.Models
{
    public class CollabileRoleClaim : IdentityRoleClaim<string>
    {
        public string Description { get; set; }
        public string Group { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public virtual CollabileRole Role { get; set; }

        public CollabileRoleClaim() : base()
        {
        }

        public CollabileRoleClaim(string roleClaimDescription = null, string roleClaimGroup = null) : base()
        {
            Description = roleClaimDescription;
            Group = roleClaimGroup;
        }
    }
}
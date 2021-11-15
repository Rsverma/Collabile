using Microsoft.AspNetCore.Identity;

namespace Collabile.DataAccess.Models.Identity
{
    public class CollabileRole : IdentityRole
    {
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public List<CollabileRoleClaim> RoleClaims { get; set; }

        public CollabileRole() : base()
        {
            RoleClaims = new List<CollabileRoleClaim>();
        }

        public CollabileRole(string roleName, string roleDescription = null) : base(roleName)
        {
            RoleClaims = new List<CollabileRoleClaim>();
            Description = roleDescription;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabile.Shared.Models
{
    public class PermissionRequest
    {
        public string RoleId { get; set; }
        public IList<RoleClaimRequest> RoleClaims { get; set; }
    }
}

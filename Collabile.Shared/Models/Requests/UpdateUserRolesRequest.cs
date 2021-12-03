using Collabile.Shared.Models.Responses;
using System.Collections.Generic;

namespace Collabile.Shared.Models.Requests
{
    public class UpdateUserRolesRequest
    {
        public string UserId { get; set; }
        public IList<UserRoleModel> UserRoles { get; set; }
    }
}

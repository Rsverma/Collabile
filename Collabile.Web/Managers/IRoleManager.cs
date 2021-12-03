using Collabile.Shared.Interfaces;
using Collabile.Shared.Models;
using Collabile.Shared.Models.Requests;
using Collabile.Shared.Models.Responses;

namespace Collabile.Web.Managers
{
    public interface IRoleManager : IManager
    {
        Task<IResult<List<RoleResponse>>> GetRolesAsync();

        Task<IResult<string>> SaveAsync(RoleRequest role);

        Task<IResult<string>> DeleteAsync(string id);

        Task<IResult<PermissionResponse>> GetPermissionsAsync(string roleId);

        Task<IResult<string>> UpdatePermissionsAsync(PermissionRequest request);
    }
}

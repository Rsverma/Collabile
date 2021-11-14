using Collabile.Shared.Helper;
using Collabile.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Collabile.Api.Services
{
    public class RoleService : IRoleService
    {
        public Task<Result<string>> DeleteAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<List<RoleResponse>>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<PermissionResponse>> GetAllPermissionsAsync(string roleId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<RoleResponse>> GetByIdAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> GetCountAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<string>> SaveAsync(RoleRequest request)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<string>> UpdatePermissionsAsync(PermissionRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}

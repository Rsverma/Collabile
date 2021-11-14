using Collabile.Shared.Helper;
using Collabile.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Collabile.Api.Services
{
    public class RoleClaimService : IRoleClaimService
    {
        public Task<Result<string>> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<List<RoleClaimResponse>>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<List<RoleClaimResponse>>> GetAllByRoleIdAsync(string roleId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<RoleClaimResponse>> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> GetCountAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<string>> SaveAsync(RoleClaimRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}

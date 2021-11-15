using Collabile.DataAccess.Models.Identity;
using Collabile.Shared.Constants;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Collabile.DataAccess.Stores
{
    public class DummyRoleStore : IRoleStore<CollabileRole>, IRoleClaimStore<CollabileRole>,IQueryableRoleStore<CollabileRole>
    {
        private readonly List<CollabileRole> _roles;

        public DummyRoleStore(IConfiguration configuration)
        {
            _roles = new List<CollabileRole>();
            _roles.Add(new CollabileRole
            {
                Id = "1",
                ConcurrencyStamp ="",
                CreatedBy ="1",
                CreatedOn = DateTime.Now,
                Description ="admin",
                LastModifiedBy ="1",
                LastModifiedOn = DateTime.Now,
                Name = RoleConstants.AdministratorRole,
                NormalizedName = RoleConstants.AdministratorRole.ToUpper()
                ,RoleClaims=new List<CollabileRoleClaim>()
                //RoleClaims
            });;
        }

        public Task<IdentityResult> CreateAsync(CollabileRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _roles.Add(role);

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> UpdateAsync(CollabileRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _roles[_roles.FindIndex(x => x.Id == role.Id)] = role;
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(CollabileRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _roles.Remove(role);

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<string> GetRoleIdAsync(CollabileRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id);
        }

        public Task<string> GetRoleNameAsync(CollabileRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task SetRoleNameAsync(CollabileRole role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            return Task.FromResult(0);
        }

        public Task<string> GetNormalizedRoleNameAsync(CollabileRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.NormalizedName);
        }

        public Task SetNormalizedRoleNameAsync(CollabileRole role, string normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;
            return Task.FromResult(0);
        }

        public async Task<CollabileRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await Task.FromResult(_roles.FirstOrDefault(x => x.Id == roleId));
        }

        public async Task<CollabileRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await Task.FromResult(_roles.FirstOrDefault(x => x.NormalizedName == normalizedRoleName));
        }

        public void Dispose()
        {
            // Nothing to dispose.
        }

        private IList<Claim> claims = new List<Claim>();

        public IQueryable<CollabileRole> Roles => _roles.AsQueryable();

        public Task<IList<Claim>> GetClaimsAsync(CollabileRole role, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(claims);
        }

        public Task AddClaimAsync(CollabileRole role, Claim claim, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimAsync(CollabileRole role, Claim claim, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}

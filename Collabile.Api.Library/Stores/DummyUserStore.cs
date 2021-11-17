using Collabile.DataAccess.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data.SqlClient;
using System.Security.Claims;
using Collabile.Shared.Constants;

namespace Collabile.DataAccess.Stores
{
    public class DummyUserStore : IUserStore<CollabileUser>, IUserEmailStore<CollabileUser>, IQueryableUserStore<CollabileUser>,
        IUserPasswordStore<CollabileUser>,IUserClaimStore<CollabileUser>,IUserRoleStore<CollabileUser>
    {
        private readonly List<CollabileUser> _users = new List<CollabileUser>();
        private readonly Dictionary<string, List<Claim>> _userClaims = new Dictionary<string, List<Claim>>();
        public DummyUserStore(IConfiguration configuration)
        {
            _users.Add(new CollabileUser
            {
                AccessFailedCount = 0,
                ConcurrencyStamp = "",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "1",
                DeletedOn = null,
                Email = "rsverma333@gmail.com",
                EmailConfirmed = true,
                FirstName = "Ramesh",
                Id = "1",
                IsActive = true,
                IsDeleted = false,
                LastModifiedBy = "1",
                LastModifiedOn = DateTime.UtcNow,
                LockoutEnabled = false,
                LastName = "Verma",
                LockoutEnd = null,
                NormalizedEmail = "RSVERMA333@GMAIL.COM",
                NormalizedUserName = "RSVERMA",
                PasswordHash = "RSVERMA",
                PhoneNumber = "7838355433",
                PhoneNumberConfirmed = true,
                SecurityStamp = "",
                TwoFactorEnabled = false,
                UserName = "rsverma"
                
            });
            _userClaims.Add("1", new List<Claim> { });
        }

        public Task AddClaimsAsync(CollabileUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            _userClaims[user.Id].AddRange(claims);
            return Task.FromResult(0);
        }

        public async Task<IList<Claim>> GetClaimsAsync(CollabileUser user, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return _userClaims[user.Id];
        }

        public async Task<IList<CollabileUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return _users;
        }

        public Task RemoveClaimsAsync(CollabileUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
            //throw new NotImplementedException();
        }

        public Task ReplaceClaimAsync(CollabileUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
            //throw new NotImplementedException();
        }

        public Task AddToRoleAsync(CollabileUser user, string roleName, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public Task RemoveFromRoleAsync(CollabileUser user, string roleName, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
        IList<string> _roles = new List<string>();

        public IQueryable<CollabileUser> Users => _users.AsQueryable();

        public Task<IList<string>> GetRolesAsync(CollabileUser user, CancellationToken cancellationToken)
        {
            _roles.Clear();
            _roles.Add(RoleConstants.AdministratorRole);
            //_roles.Add(RoleConstants.BasicRole);
            return Task.FromResult(_roles);
        }

        public Task<bool> IsInRoleAsync(CollabileUser user, string roleName, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        public Task<IList<CollabileUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            return Task.FromResult((IList<CollabileUser>)_users);
        }

        public Task<IdentityResult> CreateAsync(CollabileUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _users.Add(user);

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(CollabileUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _users.Remove(user);

            return Task.FromResult(IdentityResult.Success);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public async Task<CollabileUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await Task.FromResult(_users.FirstOrDefault(x => x.NormalizedEmail == normalizedEmail));
        }

        public async Task<CollabileUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await Task.FromResult(_users.FirstOrDefault(x => x.Id == userId));
        }

        public async Task<CollabileUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await Task.FromResult(_users.FirstOrDefault(x => x.NormalizedUserName == normalizedUserName));
        }

        public Task<string> GetEmailAsync(CollabileUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(CollabileUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task<string> GetNormalizedEmailAsync(CollabileUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task<string> GetNormalizedUserNameAsync(CollabileUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(CollabileUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<string> GetUserIdAsync(CollabileUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(CollabileUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<bool> HasPasswordAsync(CollabileUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task SetEmailAsync(CollabileUser user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.FromResult(0);
        }

        public Task SetEmailConfirmedAsync(CollabileUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task SetNormalizedEmailAsync(CollabileUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.FromResult(0);
        }

        public Task SetNormalizedUserNameAsync(CollabileUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetPasswordHashAsync(CollabileUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(CollabileUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.FromResult(0);
        }

        public Task<IdentityResult> UpdateAsync(CollabileUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _users[_users.FindIndex(x => x.Id == user.Id)] = user;
            return Task.FromResult(IdentityResult.Success);
        }

    }
}

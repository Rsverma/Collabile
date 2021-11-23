using Collabile.DataAccess.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data.SqlClient;
using System.Security.Claims;
using Collabile.Api.DataAccess;
using System.Data;

namespace Collabile.DataAccess.Stores
{
    public class UserStore : IUserStore<CollabileUser>, IUserEmailStore<CollabileUser>, IUserPasswordStore<CollabileUser>, IUserClaimStore<CollabileUser>
    {
        private readonly string _connectionString;

        public UserStore(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public Task AddClaimsAsync(CollabileUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Claim>> GetClaimsAsync(CollabileUser user, CancellationToken cancellationToken)
        {
            IList<Claim> claims = new List<Claim>();
            return Task.FromResult(claims);
        }

        public Task<IList<CollabileUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimsAsync(CollabileUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task ReplaceClaimAsync(CollabileUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> CreateAsync(CollabileUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync("dbo.spUser_Insert", new
                {
                    user.Id,
                    user.UserName,
                    user.Email,
                    user.PasswordHash,
                    user.FirstName,
                    user.LastName,
                    user.CreatedBy,
                    user.LastModifiedBy
                },commandType: CommandType.StoredProcedure);
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(CollabileUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync($@"DELETE FROM [CollabileUser]
                WHERE [Id] = @{nameof(user.Id)}", new { user.Id });
            }

            return IdentityResult.Success;
        }

        public void Dispose()
        {
            
        }

        public async Task<CollabileUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<CollabileUser>($@"SELECT * FROM [CollabileUser]
                WHERE [Email] = @{nameof(normalizedEmail)}", new { normalizedEmail });
            }
        }

        public async Task<CollabileUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<CollabileUser>($@"SELECT * FROM [CollabileUser]
                WHERE [Id] = @{nameof(userId)}", new { userId });
            }
        }

        public async Task<CollabileUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<CollabileUser>($@"SELECT * FROM [CollabileUser]
                WHERE [NormalizedUserName] = @{nameof(normalizedUserName)}", new { normalizedUserName });
            }
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
            return Task.FromResult(user.Email.ToUpper());
        }

        public Task<string> GetNormalizedUserNameAsync(CollabileUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName.ToUpper());
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
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public async Task SetEmailAsync(CollabileUser user, string email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync($@"UPDATE [CollabileUser] SET
                [Email] = @{nameof(CollabileUser.Email)},
                [LastModifiedBy] = @{nameof(CollabileUser.LastModifiedBy)},
                [LastModifiedOn] = @{nameof(CollabileUser.LastModifiedOn)},
                WHERE [Id] = @{nameof(CollabileUser.Id)}", user);
            }
        }

        public async Task SetEmailConfirmedAsync(CollabileUser user, bool confirmed, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync($@"UPDATE [CollabileUser] SET
                [EmailConfirmed] = @{nameof(CollabileUser.EmailConfirmed)},
                [LastModifiedBy] = @{nameof(CollabileUser.LastModifiedBy)},
                [LastModifiedOn] = @{nameof(CollabileUser.LastModifiedOn)},
                WHERE [Id] = @{nameof(CollabileUser.Id)}", user);
            }
        }

        public Task SetNormalizedEmailAsync(CollabileUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task SetNormalizedUserNameAsync(CollabileUser user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task SetPasswordHashAsync(CollabileUser user, string passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync($@"UPDATE [CollabileUser] SET
                [PasswordHash] = @{nameof(CollabileUser.PasswordHash)},
                [LastModifiedBy] = @{nameof(CollabileUser.LastModifiedBy)},
                [LastModifiedOn] = @{nameof(CollabileUser.LastModifiedOn)},
                WHERE [Id] = @{nameof(CollabileUser.Id)}", user);
            }
        }

        public async Task SetUserNameAsync(CollabileUser user, string userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync($@"UPDATE [CollabileUser] SET
                [UserName] = @{nameof(CollabileUser.UserName)},
                [LastModifiedBy] = @{nameof(CollabileUser.LastModifiedBy)},
                [LastModifiedOn] = @{nameof(CollabileUser.LastModifiedOn)},
                WHERE [Id] = @{nameof(CollabileUser.Id)}", user);
            }
        }

        public async Task<IdentityResult> UpdateAsync(CollabileUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync($@"UPDATE [CollabileUser] SET
                [FirstName] = @{nameof(CollabileUser.FirstName)},
                [LastName] = @{nameof(CollabileUser.LastName)},
                [Email] = @{nameof(CollabileUser.Email)},
                [PhoneNumber] = @{nameof(CollabileUser.PhoneNumber)},
                [LastModifiedBy] = @{nameof(CollabileUser.LastModifiedBy)},
                [LastModifiedOn] = @{nameof(CollabileUser.LastModifiedOn)},
                WHERE [Id] = @{nameof(CollabileUser.Id)}", user);
            }

            return IdentityResult.Success;
        }
    }
}

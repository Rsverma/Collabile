using Collabile.DataAccess.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data.SqlClient;

namespace Collabile.DataAccess.Stores
{
    public class UserStore : IUserStore<CollabileUser>, IUserEmailStore<CollabileUser>, IUserPasswordStore<CollabileUser>
    {
        private readonly string _connectionString;
        public UserStore(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<IdentityResult> CreateAsync(CollabileUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                user.Id = await connection.QuerySingleAsync<string>($@"INSERT INTO [CollabileUser] ([UserName], [NormalizedUserName], [Email],
                [NormalizedEmail], [EmailConfirmed], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled])
                VALUES (@{nameof(CollabileUser.UserName)}, @{nameof(CollabileUser.NormalizedUserName)}, @{nameof(CollabileUser.Email)},
                @{nameof(CollabileUser.NormalizedEmail)}, @{nameof(CollabileUser.EmailConfirmed)}, @{nameof(CollabileUser.PasswordHash)},
                @{nameof(CollabileUser.PhoneNumber)}, @{nameof(CollabileUser.PhoneNumberConfirmed)}, @{nameof(CollabileUser.TwoFactorEnabled)});
                SELECT CAST(SCOPE_IDENTITY() as int)", user);
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(CollabileUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync($"DELETE FROM [CollabileUser] WHERE [Id] = @{nameof(CollabileUser.Id)}", user);
            }

            return IdentityResult.Success;
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public async Task<CollabileUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<CollabileUser>($@"SELECT * FROM [CollabileUser]
                WHERE [NormalizedEmail] = @{nameof(normalizedEmail)}", new { normalizedEmail });
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

        public async Task<IdentityResult> UpdateAsync(CollabileUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync($@"UPDATE [CollabileUser] SET
                [UserName] = @{nameof(CollabileUser.UserName)},
                [NormalizedUserName] = @{nameof(CollabileUser.NormalizedUserName)},
                [Email] = @{nameof(CollabileUser.Email)},
                [NormalizedEmail] = @{nameof(CollabileUser.NormalizedEmail)},
                [EmailConfirmed] = @{nameof(CollabileUser.EmailConfirmed)},
                [PasswordHash] = @{nameof(CollabileUser.PasswordHash)},
                [PhoneNumber] = @{nameof(CollabileUser.PhoneNumber)},
                [PhoneNumberConfirmed] = @{nameof(CollabileUser.PhoneNumberConfirmed)},
                [TwoFactorEnabled] = @{nameof(CollabileUser.TwoFactorEnabled)}
                WHERE [Id] = @{nameof(CollabileUser.Id)}", user);
            }

            return IdentityResult.Success;
        }
    }
}

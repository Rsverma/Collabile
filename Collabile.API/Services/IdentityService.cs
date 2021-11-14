using Collabile.Shared.Helper;
using Collabile.Shared.Models;
using System.Threading.Tasks;

namespace Collabile.Api.Services
{
    public class IdentityService : ITokenService
    {
        public Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<TokenResponse>> LoginAsync(TokenRequest model)
        {
            throw new System.NotImplementedException();
        }
    }
}

using Collabile.Shared.Helper;
using Collabile.Shared.Models;
using System.Threading.Tasks;

namespace Collabile.Api.Services
{
    public interface ITokenService : IService
    {
        Task<Result<TokenResponse>> LoginAsync(TokenRequest model);

        Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model);
    }
}

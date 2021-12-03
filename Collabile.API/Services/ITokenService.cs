using Collabile.Shared.Helper;
using Collabile.Shared.Models;
using Collabile.Shared.Models.Requests;
using Collabile.Shared.Models.Responses;
using System.Threading.Tasks;

namespace Collabile.Api.Services
{
    public interface ITokenService : IService
    {
        Task<Result<TokenResponse>> LoginAsync(TokenRequest model);

        Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model);
    }
}

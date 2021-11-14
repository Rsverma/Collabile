using Collabile.Shared.Interfaces;
using Collabile.Shared.Models;
using Collabile.Web.Extensions;
using Collabile.Web.Library.Constants;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Collabile.Web.Managers
{
    public class AccountManager : IAccountManager
    {
        private readonly HttpClient _httpClient;

        public AccountManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult> ChangePasswordAsync(ChangePasswordRequest model)
        {
            var response = await _httpClient.PutAsJsonAsync(AccountEndpoints.ChangePassword, model);
            return await response.ToResult();
        }

        public async Task<IResult> UpdateProfileAsync(UpdateProfileRequest model)
        {
            var response = await _httpClient.PutAsJsonAsync(AccountEndpoints.UpdateProfile, model);
            return await response.ToResult();
        }

    }
}

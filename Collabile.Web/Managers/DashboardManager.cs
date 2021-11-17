using Collabile.Shared.Interfaces;
using Collabile.Shared.Models;
using Collabile.Web.Extensions;
using Collabile.Web.Library.Constants;

namespace Collabile.Web.Managers
{
    public class DashboardManager : IDashboardManager
    {
        private readonly HttpClient _httpClient;

        public DashboardManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<DashboardDataResponse>> GetDataAsync()
        {
            var response = await _httpClient.GetAsync(DashboardEndpoints.GetData);
            var data = await response.ToResult<DashboardDataResponse>();
            return data;
        }
    }
}

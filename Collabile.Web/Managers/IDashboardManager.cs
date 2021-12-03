using Collabile.Shared.Interfaces;
using Collabile.Shared.Models;
using Collabile.Shared.Models.Responses;

namespace Collabile.Web.Managers
{
    public interface IDashboardManager : IManager
    {
        Task<IResult<DashboardDataResponse>> GetDataAsync();
    }
}

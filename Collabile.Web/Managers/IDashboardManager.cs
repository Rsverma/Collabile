using Collabile.Shared.Interfaces;
using Collabile.Shared.Models;

namespace Collabile.Web.Managers
{
    public interface IDashboardManager : IManager
    {
        Task<IResult<DashboardDataResponse>> GetDataAsync();
    }
}

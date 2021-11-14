using Collabile.Shared.Interfaces;
using Collabile.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Collabile.Api.Services
{
    public interface IAuditService
    {
        Task<IResult<IEnumerable<AuditResponse>>> GetCurrentUserTrailsAsync(string userId);

        Task<IResult<string>> ExportToExcelAsync(string userId, string searchString = "", bool searchInOldValues = false, bool searchInNewValues = false);
    }
}

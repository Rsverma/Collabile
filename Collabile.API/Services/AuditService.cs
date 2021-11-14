using Collabile.Shared.Interfaces;
using Collabile.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Collabile.Api.Services
{
    public class AuditService : IAuditService
    {
        public Task<IResult<string>> ExportToExcelAsync(string userId, string searchString = "", bool searchInOldValues = false, bool searchInNewValues = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<IResult<IEnumerable<AuditResponse>>> GetCurrentUserTrailsAsync(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}

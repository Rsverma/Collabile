using Collabile.Shared.Models;
using Collabile.Shared.Models.Requests;

namespace Collabile.Api.Services
{
    public interface IUploadService
    {
        string UploadAsync(UploadRequest request);
    }
}

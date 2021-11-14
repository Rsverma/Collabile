using Collabile.Shared.Models;

namespace Collabile.Api.Services
{
    public interface IUploadService
    {
        string UploadAsync(UploadRequest request);
    }
}

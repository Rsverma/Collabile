using System.Collections.Generic;

namespace Collabile.Api.Services
{
    public interface ICurrentUserService : IService
    {
        string UserId { get; }
        List<KeyValuePair<string, string>> Claims { get; }
    }
}

namespace Collabile.Api.Services
{
    public interface ICurrentUserService : IService
    {
        string UserId { get; }
    }
}

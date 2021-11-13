using Collabile.Shared.Models;

namespace Collabile.Web.Authentication
{
    public interface IAuthService
    {
        HttpClient ApiClient { get; }
        Task<AuthenticatedUser> Login(AuthenticateModel userForAuthentication);
        Task Logout();
    }
}
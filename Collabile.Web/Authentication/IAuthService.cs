using Collabile.Shared.Models;

namespace Collabile.Web.Authentication
{
    public interface IAuthService
    {
        Task<AuthenticatedUser> Login(TokenRequest userForAuthentication);
        Task Logout();
    }
}
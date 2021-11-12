using Collabile.Shared.Models;

namespace Collabile.Web.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedUser> Login(AuthenticateModel userForAuthentication);
        Task Logout();
    }
}
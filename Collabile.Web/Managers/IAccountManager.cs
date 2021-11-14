using Collabile.Shared.Interfaces;
using Collabile.Shared.Models;

namespace Collabile.Web.Managers
{
    public interface IAccountManager : IManager
    {
        Task<IResult> ChangePasswordAsync(ChangePasswordRequest model);

        Task<IResult> UpdateProfileAsync(UpdateProfileRequest model);

    }
}

using Collabile.Shared.Interfaces;
using Collabile.Shared.Models;
using Collabile.Shared.Models.Requests;

namespace Collabile.Web.Managers
{
    public interface IAccountManager : IManager
    {
        Task<IResult> ChangePasswordAsync(ChangePasswordRequest model);

        Task<IResult> UpdateProfileAsync(UpdateProfileRequest model);

    }
}

using Collabile.Shared.Interfaces;
using Collabile.Shared.Models;
using System.Threading.Tasks;

namespace Collabile.Api.Services
{
    public interface IAccountService : IService
    {
        Task<IResult> UpdateProfileAsync(UpdateProfileRequest model, string userId);

        Task<IResult> ChangePasswordAsync(ChangePasswordRequest model, string userId);

    }
}

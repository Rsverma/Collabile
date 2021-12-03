using Collabile.Shared.Interfaces;
using Collabile.Shared.Models;
using Collabile.Shared.Models.Requests;
using System.Threading.Tasks;

namespace Collabile.Api.Services
{
    public class AccountService : IAccountService
    {
        public Task<IResult> ChangePasswordAsync(ChangePasswordRequest model, string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IResult> UpdateProfileAsync(UpdateProfileRequest model, string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}

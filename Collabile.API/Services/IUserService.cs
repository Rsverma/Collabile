using Collabile.Shared.Helper;
using Collabile.Shared.Interfaces;
using Collabile.Shared.Models;
using Collabile.Shared.Models.Requests;
using Collabile.Shared.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Collabile.Api.Services
{
    public interface IUserService : IService
    {
        Task<Result<List<UserResponse>>> GetAllAsync();

        Task<int> GetCountAsync();

        Task<IResult<UserResponse>> GetAsync(string userId);

        Task<IResult> RegisterAsync(RegisterRequest request, string origin);

        Task<IResult> ToggleUserStatusAsync(ToggleUserStatusRequest request);

        Task<IResult> UpdateRolesAsync(UpdateUserRolesRequest request);

        Task<IResult<string>> ConfirmEmailAsync(string userId, string code);

        Task<IResult> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);

        Task<IResult> ResetPasswordAsync(ResetPasswordRequest request);

        //Task<string> ExportToExcelAsync(string searchString = "");
    }
}
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Collabile.Shared.Interfaces;
using Collabile.Shared.Models;
using Collabile.Web.Extensions;
using Collabile.Web.Library.Constants;

namespace Collabile.Web.Managers
{
    public class UserManager : IUserManager
    {
        private readonly HttpClient _httpClient;

        public UserManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<List<UserResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(UserEndpoints.GetAll);
            return await response.ToResult<List<UserResponse>>();
        }

        public async Task<IResult<UserResponse>> GetAsync(string userId)
        {
            var response = await _httpClient.GetAsync(UserEndpoints.Get(userId));
            return await response.ToResult<UserResponse>();
        }

        public async Task<IResult> ToggleUserStatusAsync(ToggleUserStatusRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(UserEndpoints.ToggleUserStatus, request);
            return await response.ToResult();
        }

        public async Task<IResult<UserRolesResponse>> GetRolesAsync(string userId)
        {
            var response = await _httpClient.GetAsync(UserEndpoints.GetUserRoles(userId));
            return await response.ToResult<UserRolesResponse>();
        }

        public async Task<IResult> UpdateRolesAsync(UpdateUserRolesRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync(UserEndpoints.GetUserRoles(request.UserId), request);
            return await response.ToResult<UserRolesResponse>();
        }

        public async Task<IResult> ForgotPasswordAsync(ForgotPasswordRequest model)
        {
            var response = await _httpClient.PostAsJsonAsync(UserEndpoints.ForgotPassword, model);
            return await response.ToResult();
        }

        public async Task<IResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(UserEndpoints.ResetPassword, request);
            return await response.ToResult();
        }

        public async Task<string> ExportToExcelAsync(string searchString = "")
        {
            var response = await _httpClient.GetAsync(string.IsNullOrWhiteSpace(searchString)
                ? UserEndpoints.Export
                : UserEndpoints.ExportFiltered(searchString));
            var data = await response.Content.ReadAsStringAsync();
            return data;
        }
    }
}

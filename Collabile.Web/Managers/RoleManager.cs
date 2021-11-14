using Collabile.Shared.Interfaces;
using Collabile.Shared.Models;
using Collabile.Web.Extensions;
using Collabile.Web.Library.Constants;
using System.Net.Http.Json;

namespace Collabile.Web.Managers
{
    public class RoleManager : IRoleManager
    {
        private readonly HttpClient _httpClient;

        public RoleManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<string>> DeleteAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{RolesEndpoints.Delete}/{id}");
            return await response.ToResult<string>();
        }

        public async Task<IResult<List<RoleResponse>>> GetRolesAsync()
        {
            var response = await _httpClient.GetAsync(RolesEndpoints.GetAll);
            return await response.ToResult<List<RoleResponse>>();
        }

        public async Task<IResult<string>> SaveAsync(RoleRequest role)
        {
            var response = await _httpClient.PostAsJsonAsync(RolesEndpoints.Save, role);
            return await response.ToResult<string>();
        }

        public async Task<IResult<PermissionResponse>> GetPermissionsAsync(string roleId)
        {
            var response = await _httpClient.GetAsync(RolesEndpoints.GetPermissions + roleId);
            return await response.ToResult<PermissionResponse>();
        }

        public async Task<IResult<string>> UpdatePermissionsAsync(PermissionRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync(RolesEndpoints.UpdatePermissions, request);
            return await response.ToResult<string>();
        }
    }
}

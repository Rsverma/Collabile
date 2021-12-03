using Blazored.LocalStorage;
using Collabile.Shared.Helper;
using Collabile.Shared.Interfaces;
using Collabile.Shared.Models;
using Collabile.Shared.Models.Requests;
using Collabile.Shared.Models.Responses;
using Collabile.Web.Authentication;
using Collabile.Web.Extensions;
using Collabile.Web.Library.Constants;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Collabile.Web.Managers
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationManager(
            HttpClient httpClient,
            ILocalStorageService localStorage,
            AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<ClaimsPrincipal> CurrentUser()
        {
            var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
            return state.User;
        }

        public async Task<IResult> Login(TokenRequest model)
        {
            var response = await _httpClient.PostAsJsonAsync(TokenEndpoints.Get, model);
            var result = await response.ToResult<TokenResponse>();
            if (result.Succeeded)
            {
                var token = result.Data.Token;
                var refreshToken = result.Data.RefreshToken;
                await _localStorage.SetItemAsync(StorageConstants.AuthToken, token);
                await _localStorage.SetItemAsync(StorageConstants.RefreshToken, refreshToken);
                
                ((AuthStateProvider)this._authenticationStateProvider).MarkUserAsAuthenticated(model.Email);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return await Result.SuccessAsync();
            }
            else
            {
                return await Result.FailAsync(result.Messages);
            }
        }

        public async Task<IResult> Logout()
        {
            await _localStorage.RemoveItemAsync(StorageConstants.AuthToken);
            await _localStorage.RemoveItemAsync(StorageConstants.RefreshToken);
            await _localStorage.RemoveItemAsync(StorageConstants.UserImageURL);
            ((AuthStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
            return await Result.SuccessAsync();
        }

        public async Task<string> RefreshToken()
        {
            var token = await _localStorage.GetItemAsync<string>(StorageConstants.AuthToken);
            var refreshToken = await _localStorage.GetItemAsync<string>(StorageConstants.RefreshToken);

            var response = await _httpClient.PostAsJsonAsync(TokenEndpoints.Refresh, new RefreshTokenRequest { Token = token, RefreshToken = refreshToken });

            var result = await response.ToResult<TokenResponse>();

            if (!result.Succeeded)
            {
                throw new ApplicationException("Something went wrong during the refresh token action");
            }

            token = result.Data.Token;
            refreshToken = result.Data.RefreshToken;
            await _localStorage.SetItemAsync(StorageConstants.AuthToken, token);
            await _localStorage.SetItemAsync(StorageConstants.RefreshToken, refreshToken);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return token;
        }

        public async Task<string> TryRefreshToken()
        {
            //check if token exists
            var availableToken = await _localStorage.GetItemAsync<string>(StorageConstants.RefreshToken);
            if (string.IsNullOrEmpty(availableToken)) return string.Empty;
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var exp = user.FindFirst(c => c.Type.Equals("exp"))?.Value;
            var expTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(exp));
            var timeUTC = DateTime.UtcNow;
            var diff = expTime - timeUTC;
            if (diff.TotalMinutes <= 1)
                return await RefreshToken();
            return string.Empty;
        }

        public async Task<string> TryForceRefreshToken()
        {
            return await RefreshToken();
        }
    }
}

using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Collabile.Web.Authentication
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationState _anonymous;
        private readonly string _authTokenStorageKey;

        public AuthStateProvider(HttpClient httpClient, ILocalStorageService localStorage,IConfiguration config)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            _authTokenStorageKey = config["authTokenStorageKey"];
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>(key: _authTokenStorageKey);

            if (string.IsNullOrWhiteSpace(token))
                return _anonymous;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "bearer", token);

            return new AuthenticationState(
                user: new ClaimsPrincipal(
                    identity: new ClaimsIdentity(
                        JwtParser.ParseClaimsFromJwt(token),authenticationType: "jwtAuthType")));
        }

        public async Task NotifyUserAuthentication(string token)
        {

            await _localStorage.SetItemAsync(_authTokenStorageKey, token);
            var authenticatedUser = new ClaimsPrincipal(
                    identity: new ClaimsIdentity(
                        JwtParser.ParseClaimsFromJwt(token), authenticationType: "jwtAuthType"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public async Task NotifyUserLogout()
        {
            await _localStorage.RemoveItemAsync(_authTokenStorageKey);
            var authState = Task.FromResult(_anonymous);
            NotifyAuthenticationStateChanged(authState);
        }
    }
}

using Blazored.LocalStorage;
using Collabile.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace Collabile.Web.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _apiClient
            ;
        private readonly AuthenticationStateProvider _authStateProvider;

        public HttpClient ApiClient { get => _apiClient; }

        public AuthService(HttpClient client, AuthenticationStateProvider authStateProvider)
        {
            _apiClient = client;
            _authStateProvider = authStateProvider;
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<AuthenticatedUser> Login(AuthenticateModel userForAuthentication)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>(key:"username", value:userForAuthentication.Username),
                new KeyValuePair<string, string>(key:"password", value:userForAuthentication.Password)
            });

            var authResult = await _apiClient.PostAsJsonAsync("https://localhost:44309/api/Token/authenticate",
                new { username = userForAuthentication.Username, password = userForAuthentication.Password });

            if (authResult.IsSuccessStatusCode == false)
            {
                return null;
            }
            var authContent = await authResult.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<AuthenticatedUser>(authContent,
                options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            await ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(result.Token);

            _apiClient.DefaultRequestHeaders.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiClient.DefaultRequestHeaders.Add("Authorization", $"bearer { result.Token}");
            return result;
        }

        public async Task Logout()
        {
            await ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            _apiClient.DefaultRequestHeaders.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

    }
}

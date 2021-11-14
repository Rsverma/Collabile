//using Blazored.LocalStorage;
//using Collabile.Shared.Models;
//using Microsoft.AspNetCore.Components.Authorization;
//using System.Net.Http.Headers;
//using System.Net.Http.Json;
//using System.Text.Json;

//namespace Collabile.Web.Authentication
//{
//    public class AuthService : IAuthService
//    {
//        private readonly AuthenticationStateProvider _authStateProvider;
//        private readonly IConfiguration _config;
//        private readonly HttpClient _apiClient;

//        public AuthService(HttpClient client, AuthenticationStateProvider authStateProvider,IConfiguration config)
//        {
//            _apiClient = client;
//            _authStateProvider = authStateProvider;
//            _config = config;
//            _apiClient.DefaultRequestHeaders.Accept.Clear();
//            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

//        }

//        public async Task<AuthenticatedUser> Login(TokenRequest userForAuthentication)
//        {
//            string api = _config["api"];
//            var authResult = await _apiClient.PostAsJsonAsync(api + "Token/authenticate", userForAuthentication);

//            if (authResult.IsSuccessStatusCode == false)
//            {
//                return null;
//            }
//            var authContent = await authResult.Content.ReadAsStringAsync();

//            var result = JsonSerializer.Deserialize<AuthenticatedUser>(authContent,
//                options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

//            await ((AuthStateProvider)_stateProvider).NotifyUserAuthentication(result.Token);

//            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
//            return result;
//        }

//        public async Task Logout()
//        {
//            await ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
//            _apiClient.DefaultRequestHeaders.Authorization = null;
//        }

//    }
//}

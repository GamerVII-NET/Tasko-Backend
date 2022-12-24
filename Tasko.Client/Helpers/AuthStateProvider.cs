using System.Security.Claims;
using System.Text.Json;

namespace Tasko.Client.Helpers
{
    internal class AuthStateProvider : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = await SecureStorage.GetAsync("token");

            ClaimsIdentity identity = null;

            if (string.IsNullOrEmpty(token))
            {
                identity = new ClaimsIdentity();
            }
            else
            {
                identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            }

            var user = new ClaimsPrincipal(identity);

            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;

        }

        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        public async Task Login(string token)
        {
            //Again, this is what I'm doing, but you could do/store/save anything as part of this process
            await SecureStorage.SetAsync("token", token);

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Logout()
        {
            SecureStorage.Remove("token");
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}

using System.Security.Claims;
using System.Text.Json;

namespace Collabile.Web.Authentication
{
    public class JwtParser
    {
        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split(separator: '.')[1];

            var jsonBytes = ParseBase64WithoutPadding(payload);

            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            ExtractRolesFromJWT(claims, keyValuePairs);

            //claims.AddRange(keyValuePairs.Select(kv => new Claim(kv.Key, kv.Value.ToString())));
            return claims;
        }

        private static void ExtractRolesFromJWT(List<Claim> claims, Dictionary<string, object> keyValuePairs)
        {
            keyValuePairs.TryGetValue("role", out var role);

            if (role is not null)
            {
                string? parsedRole = role.ToString();
                if (parsedRole is not null)
                    claims.Add(new Claim(ClaimTypes.Role, parsedRole.Trim(trimChar: '"')));
            }
            keyValuePairs.Remove(ClaimTypes.Role);

            keyValuePairs.TryGetValue("unique_name", out var name);

            if (name is not null)
            {
                string? parsedName = name.ToString();
                if (parsedName is not null)
                    claims.Add(new Claim(ClaimTypes.Name, parsedName.Trim(trimChar: '"')));
            }
            keyValuePairs.Remove(ClaimTypes.Name);
        }

private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length)
            {
                case 2:
                    base64 += "==";
                    break;
                case 3:
                    base64 = "=";
                    break;

            }
            return Convert.FromBase64String(base64);
        }
    }
}

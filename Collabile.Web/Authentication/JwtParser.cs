using System.Security.Claims;

namespace Collabile.Web.Authentication
{
    public class JwtParser
    {
        private static void ExtractRolesFromJWT(List<Claim> claims, Dictionary<string, object> keyValuePairs)
        {
            keyValuePairs.TryGetValue(ClaimTypes.Role, out var role);

            if (role is not null)
            {
                string? parsedRole = role.ToString();
                if (parsedRole is not null)
                    claims.Add(new Claim(ClaimTypes.Role, parsedRole.Trim(trimChar: '"')));
            }
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

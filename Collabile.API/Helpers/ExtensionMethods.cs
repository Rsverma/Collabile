using Collabile.DataAccess.Models.Identity;
using System.Collections.Generic;
using System.Linq;

namespace Collabile.Api.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<CollabileUser> WithoutPasswords(this IEnumerable<CollabileUser> users)
        {
            if (users == null) return null;

            return users.Select(x => x.WithoutPassword());
        }

        public static CollabileUser WithoutPassword(this CollabileUser user)
        {
            if (user == null) return null;

            user.PasswordHash = null;
            return user;
        }
    }
}
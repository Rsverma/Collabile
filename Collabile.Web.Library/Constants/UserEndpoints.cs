using System;
using System.Collections.Generic;
namespace Collabile.Web.Library.Constants
{
    public static class UserEndpoints
    {
        public static string GetAll = "api/user";

        public static string Get(string userId)
        {
            return $"api/user/{userId}";
        }

        public static string GetUserRoles(string userId)
        {
            return $"api/user/roles/{userId}";
        }

        public static string ExportFiltered(string searchString)
        {
            return $"{Export}?searchString={searchString}";
        }

        public static string Export = "api/user/export";
        public static string Register = "api/user";
        public static string ToggleUserStatus = "api/user/toggle-status";
        public static string ForgotPassword = "api/user/forgot-password";
        public static string ResetPassword = "api/user/reset-password";
    }
}

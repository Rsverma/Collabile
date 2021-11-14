namespace Collabile.Web.Library.Constants
{
    public static class AccountEndpoints
    {
        public static string Register = "api/account/register";
        public static string ChangePassword = "api/account/changepassword";
        public static string UpdateProfile = "api/account/updateprofile";

        public static string GetProfilePicture(string userId)
        {
            return $"api/account/profile-picture/{userId}";
        }

        public static string UpdateProfilePicture(string userId)
        {
            return $"api/account/profile-picture/{userId}";
        }
    }
}

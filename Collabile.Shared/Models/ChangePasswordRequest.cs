using System.ComponentModel.DataAnnotations;

namespace Collabile.Shared.Models
{
    public class ChangePasswordRequest
    {
        [Required(ErrorMessage = "Old password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "New password is required")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please confirm new password")]
        public string ConfirmNewPassword { get; set; }
    }
}

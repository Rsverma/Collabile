using System.ComponentModel.DataAnnotations;

namespace Collabile.Shared.Models.Requests
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

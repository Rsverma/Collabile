using System.ComponentModel.DataAnnotations;

namespace Collabile.Shared.Models.Requests
{
    public class TokenRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
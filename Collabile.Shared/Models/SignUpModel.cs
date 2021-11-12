using System.ComponentModel.DataAnnotations;

namespace Collabile.Shared.Models
{
    public class SignUpModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace Collabile.Api.Models
{
    public class SignUpModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
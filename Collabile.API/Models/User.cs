using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collabile.Api.Models
{
    [Table("User")]
    public class User
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required][StringLength(256)]
        public string Password { get; set; }

        public string UserRole { get; set; }

        public List<string> Projects { get; set; }

        public List<TeamMember> Teams { get; set; }

        [NotMapped]
        public string Token { get; set; }
    }
}

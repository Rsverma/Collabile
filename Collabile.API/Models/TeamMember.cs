using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collabile.Api.Models
{
    [Table("TeamMember")]
    public class TeamMember
    {
        public string Team { get; set; }
        public string Member { get; set; }
        public int TeamRole { get; set; }
    }
}

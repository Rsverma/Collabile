using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collabile.Shared.Models
{
    [Table("Team")]
    public class Team
    {
        [Key][StringLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Owner { get; set; }

        public List<TeamMember> TeamMembers { get; set; }
    }
}

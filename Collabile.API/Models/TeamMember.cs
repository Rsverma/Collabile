using Collabile.Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collabile.Api.Models
{
    [Table("TeamMember")]
    public class TeamMember
    {
        public string Team { get; set; }
        public string Member { get; set; }
        public TeamRole TeamRole { get; set; }
    }
}

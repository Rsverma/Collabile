using System.ComponentModel.DataAnnotations;

namespace Collabile.Shared.Models.Responses
{
    public class RoleResponse
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

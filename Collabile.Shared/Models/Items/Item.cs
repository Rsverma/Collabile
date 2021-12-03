using Collabile.Shared.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Collabile.Shared.Models.Items
{
    public abstract class Item
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public ItemType ItemType { get; set; }
        public string? Assignee { get; set; }

        [Required]
        public string Reporter { get; set; }

        public List<Comment> Comments { get; set; }

        public List<Attachment> Attachments { get; set; }

    }
}

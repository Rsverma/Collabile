using Collabile.Shared.Enums;
using Collabile.Shared.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collabile.DataAccess.Models
{
    [Table("Epic")]
    public class Epic
    {
        [Key]
        public int Id { get; set; }

        [Required][StringLength(500)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public EpicState State { get; set; }

        public int Priority { get; set; }

        [Required][StringLength(20)]
        public string ProjectArea { get; set; }

        public string Project { get; set; }

        public List<Comment> Comments { get; set; }

        public List<Attachment> Attachments { get; set; }

        public List<string> Tags{ get; set; }

        public List<TaskSummary> Features { get; set; }

    }
}

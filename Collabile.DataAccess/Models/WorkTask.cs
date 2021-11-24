using Collabile.Shared.Enums;
using Collabile.Shared.Helper;
using Collabile.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collabile.DataAccess.Models
{
    [Table("Task")]
    public class WorkTask
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public TaskType Type { get; set; }

        public TaskState State { get; set; }

        public ItemSummary ParentStory { get; set; }

        public decimal EstimatedEffort { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreateDate { get; set; }
        public int? Assignee { get; set; }
        public int Reporter { get; set; }

        public List<WorkLog> WorkLogs { get; set; }

        public List<Comment> Comments { get; set; }

        public List<Attachment> Attachments { get; set; }
    }
}

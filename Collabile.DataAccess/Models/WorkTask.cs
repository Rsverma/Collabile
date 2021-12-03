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
    public class WorkTask: Item
    {
        public TaskType Type { get; set; }

        public TaskState State { get; set; }

        public ItemSummary ParentStory { get; set; }

        public decimal EstimatedEffort { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreateDate { get; set; }

        public List<WorkLog> WorkLogs { get; set; }
    }
}

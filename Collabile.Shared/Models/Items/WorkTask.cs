using Collabile.Shared.Enums;
using Collabile.Shared.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collabile.Shared.Models.Items
{
    [Table("Task")]
    public class WorkTask: Item
    {
        public WorkTask()
        {
            ItemType = ItemType.WorkTask;
        }
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

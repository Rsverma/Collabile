using Collabile.Shared.Enums;
using Collabile.Shared.Helper;
using Collabile.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collabile.Shared.Models.Items
{
    [Table("Story")]
    public class Story : Item
    {
        public Story()
        {
            ItemType = ItemType.Story;
        }

        public StoryType Type { get; set; }

        public StoryState State { get; set; }
        public ItemSummary ParentFeature { get; set; }
        public Sprint Sprint { get; set; }
        public ReleaseSummary Release { get; set; }
        public int Priority { get; set; }
        [Required]
        public string AcceptanceCriteria { get; set; }
        public DateTime CreateDate { get; set; }

        public List<ItemSummary> RelatedStories { get; set; }

        public List<string> Tags { get; set; }

        public List<ItemSummary> Tasks { get; set; }
    }
}

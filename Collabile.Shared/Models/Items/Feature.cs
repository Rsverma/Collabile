using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Collabile.Shared.Enums;
using Collabile.Shared.Helper;
using Collabile.Shared.Models;

namespace Collabile.Shared.Models.Items
{
    [Table("Feature")]
    public class Feature : Item
    {
        public Feature()
        {
            ItemType = ItemType.Feature;
        }
        public FeatureType Type { get; set; }
        public FeatureState State { get; set; }
        public ItemSummary ParentEpic { get; set; }
        public ReleaseSummary Release { get; set; }
        public int Priority { get; set; }
        [Required][StringLength(20)]
        public string BusinessValue { get; set; }

        public string Project { get; set; }

        public List<string> Tags { get; set; }

        public List<ItemSummary> Stories { get; set; }
    }
}

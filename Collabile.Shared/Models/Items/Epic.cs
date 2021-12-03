using Collabile.Shared.Enums;
using Collabile.Shared.Helper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collabile.Shared.Models.Items
{
    [Table("Epic")]
    public class Epic : Item
    {
        public Epic()
        {
            ItemType = ItemType.Epic;
        }
        public EpicState State { get; set; }

        public int Priority { get; set; }

        [Required][StringLength(20)]
        public string ProjectArea { get; set; }

        public string Project { get; set; }
        public List<string> Tags{ get; set; }

        public List<ItemSummary> Features { get; set; }

    }
}

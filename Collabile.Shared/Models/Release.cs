using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collabile.Shared.Models
{
    [Table("Release")]
    public class Release
    {
        [Key]
        public int Id { get; set; }

        public int Index { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(20)]
        public string Project { get; set; }

        public List<Sprint> Sprints { get; set; }
    }
}

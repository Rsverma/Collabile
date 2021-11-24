using Collabile.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collabile.Api.Models
{
    [Table("Project")]
    public class Project
    {
        [Key][StringLength(20)]
        public string Key { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public bool IsPublic { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public int SprintDays { get; set; }

        public string Owner { get; set; }

        public List<string> ProjectShareholders { get; set; }

        public List<Release> Releases { get; set; }
    }
}

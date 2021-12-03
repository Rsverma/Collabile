using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collabile.Shared.Models
{
    [Table("WorkLog")]
    public class WorkLog
    {
        [Key]
        public int Id { get; set; }
        public string User { get; set; }

        public decimal Effort { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        public string Description { get; set; }
    }
}

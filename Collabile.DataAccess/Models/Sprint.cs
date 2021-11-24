﻿using Collabile.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collabile.DataAccess.Models
{
    [Table("Sprint")]
    public class Sprint
    {
        [Key]
        public int Id { get; set; }
        public int Index { get; set; }
        public ReleaseSummary Release { get; set; }
    }
}

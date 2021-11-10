﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Collabile.Shared.Entities;

namespace Collabile.Api.Models
{
    [Table("Feature")]
    public class Feature
    {
        [Key]
        public int Id { get; set; }

        [Required][StringLength(500)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public FeatureType Type { get; set; }
        public FeatureState State { get; set; }
        public TaskSummary ParentEpic { get; set; }
        public ReleaseSummary Release { get; set; }
        public int Priority { get; set; }
        [Required][StringLength(20)]
        public string BusinessValue { get; set; }
        public string Assignee { get; set; }

        public string Project { get; set; }

        public List<Comment> Comments { get; set; }

        public List<Attachment> Attachments { get; set; }

        public List<string> Tags { get; set; }

        public List<TaskSummary> Stories { get; set; }
    }
}

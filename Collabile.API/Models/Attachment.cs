using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collabile.Api.Models
{
    [Table("Attachment")]
    public class Attachment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime AttachedAt { get; set; }

        [Required][StringLength(256)]
        public string Path { get; set; }
    }
}

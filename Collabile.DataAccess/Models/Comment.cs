using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Collabile.DataAccess.Models
{
    [Table("Comment")]
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public string User { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        public string Description { get; set; }
    }
}

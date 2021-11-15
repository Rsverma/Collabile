using Collabile.Shared.Interfaces;
using Collabile.Shared.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collabile.Api.Models
{
    public class CollabileUser : IdentityUser<string>, IChatUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string CreatedBy { get; set; }

        [Column(TypeName = "text")]
        public string ProfilePictureDataUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public virtual ICollection<ChatHistory<CollabileUser>> ChatHistoryFromUsers { get; set; }
        public virtual ICollection<ChatHistory<CollabileUser>> ChatHistoryToUsers { get; set; }

        public CollabileUser()
        {
            ChatHistoryFromUsers = new List<ChatHistory<CollabileUser>>();
            ChatHistoryToUsers = new List<ChatHistory<CollabileUser>>();
        }
    }
}

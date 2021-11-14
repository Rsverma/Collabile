using Collabile.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabile.Shared.Models
{
    public class ChatUserResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string ProfilePictureDataUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsOnline { get; set; }
        public virtual ICollection<ChatHistory<IChatUser>> ChatHistoryFromUsers { get; set; }
        public virtual ICollection<ChatHistory<IChatUser>> ChatHistoryToUsers { get; set; }
    }
}

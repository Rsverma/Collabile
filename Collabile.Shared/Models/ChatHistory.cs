using Collabile.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabile.Shared.Models
{
    public partial class ChatHistory<TUser> : IChatHistory<TUser> where TUser : IChatUser
    {
        public long Id { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual TUser FromUser { get; set; }
        public virtual TUser ToUser { get; set; }
    }
}

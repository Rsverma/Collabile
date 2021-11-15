using Collabile.Api.Services;
using Collabile.Shared.Helper;
using Collabile.Shared.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Collabile.Shared.Models
{
    public class ChatService : IChatService
    {
        public Task<Result<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string userId, string contactId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<IEnumerable<ChatUserResponse>>> GetChatUsersAsync(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> message)
        {
            throw new System.NotImplementedException();
        }
    }
}

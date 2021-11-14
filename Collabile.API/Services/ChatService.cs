using Collabile.Shared.Helper;
using Collabile.Shared.Interfaces;
using Collabile.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Collabile.Api.Services
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

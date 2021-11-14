using Collabile.Shared.Helper;
using Collabile.Shared.Interfaces;
using Collabile.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Collabile.Api.Services
{
    public interface IChatService
    {
        Task<Result<IEnumerable<ChatUserResponse>>> GetChatUsersAsync(string userId);

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> message);

        Task<Result<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string userId, string contactId);
    }
}

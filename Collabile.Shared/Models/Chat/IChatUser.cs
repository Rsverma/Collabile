using System.ComponentModel.DataAnnotations.Schema;

namespace Collabile.Shared.Models.Chat
{
    public interface IChatUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}

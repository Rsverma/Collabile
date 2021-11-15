using System.ComponentModel.DataAnnotations.Schema;

namespace Collabile.Shared.Models
{
    public interface IChatUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}

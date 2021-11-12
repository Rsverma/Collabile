using Collabile.Api.Models;
using Collabile.Shared.Models;
using System.Collections.Generic;

namespace Collabile.Api.Services
{
    public interface IUserService
    {
        AuthenticatedUser Authenticate(string username, string password);

        bool CreateUser(User user);

        void UpdateUser(User user);

        void DeleteUser(string username);

        IEnumerable<User> GetAll();
    }
}
using Collabile.Api.Models;
using System.Collections.Generic;

namespace Collabile.Api.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);

        User CreateUser(User user);

        void UpdateUser(User user);

        void DeleteUser(string username);

        IEnumerable<User> GetAll();
    }
}
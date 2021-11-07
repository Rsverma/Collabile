using Collabile.Shared.Entities;
using System.Collections.Generic;

namespace Collabile.Api.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);

        User CreateUser(User user);

        void UpdateUser(User user);

        void DeleteUser(int userId);

        IEnumerable<User> GetAll();
    }
}
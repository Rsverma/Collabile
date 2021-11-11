using Collabile.Api.DataAccess;
using Collabile.Api.Helpers;
using Collabile.Api.Models;
using Collabile.Shared.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Collabile.Api.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly ISqlDataAccess _sql;

        public UserService(IOptions<AppSettings> appSettings, ISqlDataAccess sql)
        {
            _appSettings = appSettings.Value;
            _sql = sql;
        }

        public User Authenticate(string username, string password)
        {
            string passHash = _sql.LoadSingle<string, dynamic>("dbo.spUserLookup", new { username });

            // return null if user not found
            if (string.IsNullOrEmpty(passHash) || !EncryptionHelper.Validate(password, passHash))
                return null;
            User user = _sql.LoadSingle<User, dynamic>("dbo.spUser_GetById", new { username });
            UpdateUserToken(user);

            return user.WithoutPassword();
        }

        private void UpdateUserToken(User user)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.UserRole)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
        }

        public IEnumerable<User> GetAll()
        {
            var users = _sql.LoadData<User, dynamic>("dbo.spUser_GetAll", new { });
            return users.WithoutPasswords();
        }

        public User CreateUser(User user)
        {
            string encrypted = EncryptionHelper.Encrypt(user.Password);
            if (!string.IsNullOrEmpty(encrypted) || !string.IsNullOrEmpty(user.Username))
            {
                int count = _sql.SaveDataScalar("dbo.spUser_Insert", new { user.Username, Password = encrypted, user.UserRole });
                if (count > 0)
                {
                    UpdateUserToken(user);
                    return user.WithoutPassword();
                }
            }
            return null;
        }

        public void UpdateUser(User user)
        {
            string encrypted = EncryptionHelper.Encrypt(user.Password);
            if (!string.IsNullOrEmpty(user.Username))
            {
                _ = _sql.SaveData("dbo.spUser_Update", new { user.Username, Password = encrypted, user.UserRole });
            }
        }

        public void DeleteUser(string username)
        {
            _ = _sql.SaveData("dbo.spUser_Delete", new { Username = username });
        }
    }
}
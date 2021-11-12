using Collabile.Api.DataAccess;
using Collabile.Api.Helpers;
using Collabile.Api.Models;
using Collabile.Shared.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dapper;

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

        public AuthenticatedUser Authenticate(string username, string password)
        {

            UserCred passRole = _sql.LoadSingle<UserCred, dynamic>("dbo.spUserLookup", new { username });

            // return null if user not found
            if (string.IsNullOrEmpty(passRole.Password) || !EncryptionHelper.Validate(password, passRole.Password))
                return null;
            //SqlMapper.GridReader reader = _sql.LoadMultiple<dynamic>("dbo.spUser_GetById", new { username });

            //IEnumerable<User> users = reader.Read<User>();
            //User user  = users.GetEnumerator().Current;
            //user.Projects = reader.Read<string>().AsList();
            //user.Teams = reader.Read<TeamMember>().AsList();

            return new AuthenticatedUser(username, GetUserToken(username, passRole.UserRole));
        }

        private string GetUserToken(string username, string role)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public IEnumerable<User> GetAll()
        {
            var users = _sql.LoadData<User, dynamic>("dbo.spUser_GetAll", new { });
            return users.WithoutPasswords();
        }

        public bool CreateUser(User user)
        {
            string encrypted = EncryptionHelper.Encrypt(user.Password);
            if (!string.IsNullOrEmpty(encrypted) || !string.IsNullOrEmpty(user.Username))
            {
                int count = _sql.SaveData("dbo.spUser_Insert", new { user.Username, Password = encrypted, user.UserRole });
                return count > 0;
            }
            return false;
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
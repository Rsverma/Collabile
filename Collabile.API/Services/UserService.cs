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
using System.Threading.Tasks;
using Collabile.Shared.Interfaces;
using Collabile.Shared.Helper;

namespace Collabile.Api.Services
{
    public class UserService : IUserService
    {
        private readonly AppConfiguration _appSettings;
        private readonly ISqlDataAccess _sql;
        //private List<BlazorHeroUser>

        public UserService(IOptions<AppConfiguration> appSettings, ISqlDataAccess sql)
        {
            _appSettings = appSettings.Value;
            _sql = sql;
        }

        public async Task<IResult<string>> ConfirmEmailAsync(string userId, string code)
        {
            return await Result<string>.SuccessAsync();
        }

        public async Task<string> ExportToExcelAsync(string searchString = "")
        {
            await Task.CompletedTask;
            return "";
        }

        public async Task<IResult> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
        {
            return await Result<string>.SuccessAsync();
        }

        public async Task<Result<List<UserResponse>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IResult<UserResponse>> GetAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetCountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IResult<UserRolesResponse>> GetRolesAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> RegisterAsync(RegisterRequest request, string origin)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> ToggleUserStatusAsync(ToggleUserStatusRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> UpdateRolesAsync(UpdateUserRolesRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
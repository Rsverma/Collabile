using Collabile.Api.DataAccess;
using Collabile.Api.Helpers;
using Collabile.Api.Models;
using Collabile.Shared.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using Collabile.Shared.Interfaces;
using Collabile.Shared.Helper;
using Microsoft.AspNetCore.Identity;
using Collabile.DataAccess.Models.Identity;
using AutoMapper;
using Microsoft.AspNetCore.WebUtilities;
using Collabile.DataAccess.Models;
using Hangfire;
using System.Text.Encodings.Web;
using System.Globalization;
using Collabile.Shared.Constants;

namespace Collabile.Api.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<CollabileUser> _userManager;
        //private readonly IMailService _mailService;
        private readonly IExcelService _excelService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public UserService(
            UserManager<CollabileUser> userManager,
            IMapper mapper,
            RoleManager<CollabileRole> roleManager,
            IExcelService excelService,
            ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _excelService = excelService;
            _currentUserService = currentUserService;
        }

        public async Task<Result<List<UserResponse>>> GetAllAsync()
        {
            var users = _userManager.Users.ToList();
            var result = _mapper.Map<List<UserResponse>>(users);
            return await Result<List<UserResponse>>.SuccessAsync(result);
        }

        public async Task<IResult> RegisterAsync(RegisterRequest request, string origin)
        {
            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                return await Result.FailAsync(string.Format("Username {0} is already taken.", request.UserName));
            }
            var user = new CollabileUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                IsActive = request.ActivateUser,
                EmailConfirmed = request.AutoConfirmEmail
            };

            if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
            {
                var userWithSamePhoneNumber = _userManager.Users.FirstOrDefault(x => x.PhoneNumber == request.PhoneNumber);
                if (userWithSamePhoneNumber != null)
                {
                    return await Result.FailAsync(string.Format("Phone number {0} is already registered.", request.PhoneNumber));
                }
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RoleConstants.BasicRole);
                    //if (!request.AutoConfirmEmail)
                    //{
                    //    var verificationUri = await SendVerificationEmail(user, origin);
                    //    var mailRequest = new MailRequest
                    //    {
                    //        From = "mail@codewithmukesh.com",
                    //        To = user.Email,
                    //        Body = string.Format("Please confirm your account by <a href='{0}'>clicking here</a>.", verificationUri),
                    //        Subject = "Confirm Registration"
                    //    };
                    //    BackgroundJob.Enqueue(() => _mailService.SendAsync(mailRequest));
                    //    return await Result<string>.SuccessAsync(user.Id, string.Format("User {0} Registered. Please check your Mailbox to verify!", user.UserName));
                    //}
                    return await Result<string>.SuccessAsync(user.Id, string.Format("User {0} Registered.", user.UserName));
                }
                else
                {
                    return await Result.FailAsync(result.Errors.Select(a => a.Description.ToString()).ToList());
                }
            }
            else
            {
                return await Result.FailAsync(string.Format("Email {0} is already registered.", request.Email));
            }
        }

        private async Task<string> SendVerificationEmail(CollabileUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "api/identity/user/confirm-email/";
            var endpointUri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(endpointUri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);
            return verificationUri;
        }

        public async Task<IResult<UserResponse>> GetAsync(string userId)
        {
            try
            {
                CollabileUser user = await _userManager.FindByIdAsync(userId);
                UserResponse resp = new UserResponse
                {
                    Id = user.Id,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    FirstName = user.FirstName,
                    IsActive = !user.IsDeleted,
                    LastName = user.LastName,  
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName
                };

                //var result = _mapper.Map<CollabileUser, UserResponse>(user);
                return await Result<UserResponse>.SuccessAsync(resp);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IResult> ToggleUserStatusAsync(ToggleUserStatusRequest request)
        {
            var user = _userManager.Users.Where(u => u.Id == request.UserId).FirstOrDefault();
            var isAdmin = await _userManager.IsInRoleAsync(user, RoleConstants.AdministratorRole);
            if (isAdmin)
            {
                return await Result.FailAsync("Administrators Profile's Status cannot be toggled");
            }
            if (user != null)
            {
                user.IsActive = request.ActivateUser;
                var identityResult = await _userManager.UpdateAsync(user);
            }
            return await Result.SuccessAsync();
        }

        public async Task<IResult> UpdateRolesAsync(UpdateUserRolesRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            var roles = await _userManager.GetRolesAsync(user);
            var selectedRoles = request.UserRoles.Where(x => x.Selected).ToList();

            var currentUser = await _userManager.FindByIdAsync(_currentUserService.UserId);
            if (!await _userManager.IsInRoleAsync(currentUser, RoleConstants.AdministratorRole))
            {
                return await Result.FailAsync("Non admin users are not allowed to change roles.");
            }

            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            result = await _userManager.AddToRolesAsync(user, selectedRoles.Select(y => y.RoleName));
            return await Result.SuccessAsync("Roles Updated");
        }

        public async Task<IResult<string>> ConfirmEmailAsync(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return await Result<string>.SuccessAsync(user.Id, string.Format("Account Confirmed for {0}. You can now use the /api/identity/token endpoint to generate JWT.", user.Email));
            }
            else
            {
                throw new ApiException(string.Format("An error occurred while confirming {0}", user.Email));
            }
        }

        public async Task<IResult> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // Don't reveal that the user does not exist or is not confirmed
                return await Result.FailAsync("An Error has occurred!");
            }
            // For more information on how to enable account confirmation and password reset please
            // visit https://go.microsoft.com/fwlink/?LinkID=532713
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "account/reset-password";
            var endpointUri = new Uri(string.Concat($"{origin}/", route));
            var passwordResetURL = QueryHelpers.AddQueryString(endpointUri.ToString(), "Token", code);
            var mailRequest = new MailRequest
            {
                Body = string.Format("Please reset your password by <a href='{0}>clicking here</a>.", HtmlEncoder.Default.Encode(passwordResetURL)),
                Subject = "Reset Password",
                To = request.Email
            };
            //BackgroundJob.Enqueue(() => _mailService.SendAsync(mailRequest));
            return await Result.SuccessAsync("Password Reset Mail has been sent to your authorized Email.");
        }

        public async Task<IResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return await Result.FailAsync("An Error has occured!");
            }

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
            if (result.Succeeded)
            {
                return await Result.SuccessAsync("Password Reset Successful!");
            }
            else
            {
                return await Result.FailAsync("An Error has occured!");
            }
        }

        public Task<int> GetCountAsync()
        {
            var count = _userManager.Users.Count();
            return Task.FromResult(count);
        }

        //public async Task<string> ExportToExcelAsync(string searchString = "")
        //{
        //    var userSpec = new UserFilterSpecification(searchString);
        //    var users = await _userManager.Users
        //        .Specify(userSpec)
        //        .OrderByDescending(a => a.CreatedOn)
        //        .ToListAsync();
        //    var result = await _excelService.ExportAsync(users, sheetName: "Users",
        //        mappers: new Dictionary<string, Func<CollabileUser, object>>
        //        {
        //            { "Id", item => item.Id },
        //            { "FirstName", item => item.FirstName },
        //            { "LastName", item => item.LastName },
        //            { "UserName", item => item.UserName },
        //            { "Email", item => item.Email },
        //            { "EmailConfirmed", item => item.EmailConfirmed },
        //            { "PhoneNumber", item => item.PhoneNumber },
        //            { "PhoneNumberConfirmed", item => item.PhoneNumberConfirmed },
        //            { "IsActive", item => item.IsActive },
        //            { "CreatedOn (Local)", item => DateTime.SpecifyKind(item.CreatedOn, DateTimeKind.Utc).ToLocalTime().ToString("G", CultureInfo.CurrentCulture) },
        //            { "CreatedOn (UTC)", item => item.CreatedOn.ToString("G", CultureInfo.CurrentCulture) },
        //        });

        //    return result;
        //}
    }
}
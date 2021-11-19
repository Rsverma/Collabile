﻿using MudBlazor;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.JSInterop;
using Collabile.Shared.Models;
using Collabile.Shared.Constants;

namespace Collabile.Web.Pages.Admin
{
    public partial class ManageUsers
    {
        private List<UserResponse> _userList = new();
        private UserResponse _user = new();
        private string _searchString = "";
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;

        private ClaimsPrincipal _currentUser;
        private bool _canCreateUsers = true;
        private bool _canSearchUsers = true;
        private bool _canExportUsers = true;
        private bool _canViewRoles = true;
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            //_canCreateUsers = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Users.Create)).Succeeded;
            //_canSearchUsers = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Users.Search)).Succeeded;
            //_canExportUsers = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Users.Export)).Succeeded;
            //_canViewRoles = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Roles.View)).Succeeded;

            await GetUsersAsync();
            _loaded = true;
        }

        private async Task GetUsersAsync()
        {
            var response = await _userManager.GetAllAsync();
            if (response.Succeeded)
            {
                _userList = response.Data.ToList();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private bool Search(UserResponse user)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (user.FirstName?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (user.LastName?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (user.Email?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (user.PhoneNumber?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (user.UserName?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }

        private async Task ExportToExcel()
        {
            var base64 = await _userManager.ExportToExcelAsync(_searchString);
            await _jsRuntime.InvokeVoidAsync("Download", new
            {
                ByteArray = base64,
                FileName = $"{nameof(ManageUsers).ToLower()}_{DateTime.Now:ddMMyyyyHHmmss}.xlsx",
                MimeType = ApplicationConstants.MimeTypes.OpenXml
            });
            _snackBar.Add(string.IsNullOrWhiteSpace(_searchString)
                ? "Users exported"
                : "Filtered Users exported", Severity.Success);
        }

        private async Task InvokeModal()
        {
            var parameters = new DialogParameters();
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddUser>("Register New User", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await GetUsersAsync();
            }
        }

        private void ViewProfile(string userId)
        {
            _navigationManager.NavigateTo($"/user-profile/{userId}");
        }

        private void ManageRoles(string userId, string email)
        {
            _navigationManager.NavigateTo($"/identity/user-roles/{userId}");
        }
    }
}
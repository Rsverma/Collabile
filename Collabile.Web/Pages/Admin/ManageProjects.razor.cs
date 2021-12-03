using MudBlazor;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.JSInterop;
using Collabile.Shared.Models;
using Collabile.Shared.Constants;
using Collabile.Shared.Helper;
using Collabile.Shared.Models.Responses;

namespace Collabile.Web.Pages.Admin
{
    public partial class ManageProjects
    {
        private List<ProjectSummary> _projectsList = new();
        private UserResponse _user = new();
        private string _searchString = "";
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;

        private bool _isAdmin;
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            _isAdmin = (await _authenticationManager.CurrentUser()).FindFirst(ClaimTypes.Role).Value.Equals(RoleConstants.AdministratorRole);

            await GetProjectsAsync();
            _loaded = true;
        }

        private async Task GetProjectsAsync()
        {
            var response = await _userManager.GetAllAsync();
            if (response.Succeeded)
            {
                _projectsList = new List<ProjectSummary>();//response.Data.ToList();
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
                await GetProjectsAsync();
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
using Collabile.Shared.Constants;
using Collabile.Web.Managers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;

namespace Collabile.Web.Pages.Main
{
    public partial class Projects
    {
        [Inject] private IDashboardManager DashboardManager { get; set; }
        [Parameter] public List<string> OwnedProjects { get; set; } = new List<string>{ "Training", "Sirius", "Stratos" };
        [Parameter] public List<string> WorkingProjects { get; set; } = new List<string> { "Bootcamp", "Onboarding", "Rhinestone" };
        [Parameter] public List<string> ShareHolderProjects { get; set; } = new List<string> { };
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            //await LoadDataAsync();
            
            _loaded = true;
            await Task.CompletedTask;
            //HubConnection = new HubConnectionBuilder()
            //.WithUrl(_navigationManager.ToAbsoluteUri(ApplicationConstants.SignalR.HubUrl))
            //.Build();
            //HubConnection.On(ApplicationConstants.SignalR.ReceiveUpdateDashboard, async () =>
            //{
            //    await LoadDataAsync();
            //    StateHasChanged();
            //});
            //await HubConnection.StartAsync();
        }

        private async Task LoadDataAsync()
        {
            var response = await DashboardManager.GetDataAsync();
            if (response.Succeeded)
            {
                //ProjectCount = response.Data.ProductCount;
                //TeamCount = response.Data.BrandCount;
                //SprintStoryCount = response.Data.DocumentCount;
                
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }
    }
}

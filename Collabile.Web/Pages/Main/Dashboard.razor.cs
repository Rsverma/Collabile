using Collabile.Shared.Constants;
using Collabile.Web.Managers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;

namespace Collabile.Web.Pages.Main
{
    public partial class Dashboard
    {
        [Inject] private IDashboardManager DashboardManager { get; set; }

        [CascadingParameter] private HubConnection HubConnection { get; set; }
        [Parameter] public int ProjectCount { get; set; } = 5;
        [Parameter] public int TeamCount { get; set; } = 3;
        [Parameter] public int SprintStoryCount { get; set; } = 10;
        [Parameter] public int SprintTotalTasks { get; set; } = 45;
        [Parameter] public int SprintCompletedTasks { get; set; } = 30;
        [Parameter] public int OverdueIssues { get; set; } = 1;
        [Parameter] public int InProgressTasks { get; set; } = 3;

        private readonly string[] _dataEnterBarChartXAxisLabels = { "Sprint1", "Sprint2", "Sprint3", "Sprint4", "Sprint5", };
        private readonly List<ChartSeries> _dataEnterBarChartSeries = new();
        private readonly ChartOptions _chartOptions = new ChartOptions();
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            //await LoadDataAsync();
            _chartOptions.YAxisLines = true;
            _chartOptions.YAxisTicks = 3;
            _dataEnterBarChartSeries.Add(new ChartSeries { Name = "User Story", Data = new double[]{ 6, 5, 8, 4, 5} });
            _dataEnterBarChartSeries.Add(new ChartSeries { Name = "Defect Story", Data = new double[] { 3, 3, 5, 3, 7 } });
            _dataEnterBarChartSeries.Add(new ChartSeries { Name = "Technical Story", Data = new double[] { 2, 4, 2, 1, 5 } });
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
                ProjectCount = response.Data.ProductCount;
                TeamCount = response.Data.BrandCount;
                SprintStoryCount = response.Data.DocumentCount;
                SprintTotalTasks = response.Data.DocumentTypeCount;
                SprintCompletedTasks = response.Data.DocumentExtendedAttributeCount;
                OverdueIssues = response.Data.UserCount;
                InProgressTasks = response.Data.RoleCount;
                foreach (var item in response.Data.DataEnterBarChart)
                {
                    _dataEnterBarChartSeries
                        .RemoveAll(x => x.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase));
                    _dataEnterBarChartSeries.Add(new ChartSeries { Name = item.Name, Data = item.Data });
                }
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

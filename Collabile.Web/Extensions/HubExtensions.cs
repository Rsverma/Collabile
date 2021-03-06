using Collabile.Shared.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace Collabile.Web.Extensions
{
    public static class HubExtensions
    {
        public static HubConnection TryInitialize(this HubConnection hubConnection, NavigationManager navigationManager)
        {
            if (hubConnection == null)
            {
                hubConnection = new HubConnectionBuilder()
                                  .WithUrl("https://localhost:44332" +  ApplicationConstants.SignalR.HubUrl)
                                  .Build();
            }
            return hubConnection;
        }
    }
}

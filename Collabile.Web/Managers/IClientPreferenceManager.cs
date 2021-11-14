using Collabile.Shared.Interfaces;
using MudBlazor;

namespace Collabile.Web.Managers
{
    public interface IClientPreferenceManager : IPreferenceManager
    {
        Task<MudTheme> GetCurrentThemeAsync();

        Task<bool> ToggleDarkModeAsync();
    }
}

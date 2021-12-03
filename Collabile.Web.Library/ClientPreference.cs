using Collabile.Shared.Constants;
using Collabile.Shared.Interfaces;

namespace Collabile.Web.Library
{
    public record ClientPreference : IPreference
    {
        public bool IsDarkMode { get; set; }
        public bool IsRTL { get; set; }
        public bool IsDrawerOpen { get; set; }
        public string PrimaryColor { get; set; }
        public string LanguageCode { get; set; } = "en-US";
    }
}
using System;
using Windows.UI.Xaml;

namespace DomoticzUWP.Services.SettingsServices
{
    public interface ISettingsService
    {
        ApplicationTheme AppTheme { get; set; }
        String DomoticzHost { get; set; }
        String DomoticzUsername { get; set; }
        String DomoticzPassword { get; set; }
        TimeSpan CacheMaxDuration { get; set; }
    }
}

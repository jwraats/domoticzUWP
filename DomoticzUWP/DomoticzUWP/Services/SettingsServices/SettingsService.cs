using System;
using Windows.UI.Xaml;

namespace DomoticzUWP.Services.SettingsServices
{
    // DOCS: https://github.com/Windows-XAML/Template10/wiki/Docs-%7C-SettingsService
    public partial class SettingsService : ISettingsService
    {
        public static SettingsService Instance { get; }
        static SettingsService()
        {
            // implement singleton pattern
            Instance = Instance ?? new SettingsService();
        }

        Template10.Services.SettingsService.ISettingsHelper _helper;
        private SettingsService()
        {
            _helper = new Template10.Services.SettingsService.SettingsHelper();
        }

        public ApplicationTheme AppTheme
        {
            get
            {
                var theme = ApplicationTheme.Dark;
                var value = _helper.Read<string>(nameof(AppTheme), theme.ToString());
                return Enum.TryParse<ApplicationTheme>(value, out theme) ? theme : ApplicationTheme.Dark;
            }
            set
            {
                _helper.Write(nameof(AppTheme), value.ToString());
            }
        }

        public String DomoticzHost
        {
            get
            {
                var host = "localhost";
                var value = _helper.Read<string>(nameof(DomoticzHost), host.ToString());
                return value;
            }
            set
            {
                _helper.Write(nameof(DomoticzHost), value.ToString());
            }
        }

        public TimeSpan CacheMaxDuration
        {
            get { return _helper.Read<TimeSpan>(nameof(CacheMaxDuration), TimeSpan.FromDays(2)); }
            set
            {
                _helper.Write(nameof(CacheMaxDuration), value);
                ApplyCacheMaxDuration(value);
            }
        }

        public String DomoticzUsername
        {
            get
            {
                var username = "";
                var value = _helper.Read<string>(nameof(DomoticzUsername), username.ToString());
                return value;
            }
            set
            {
                _helper.Write(nameof(DomoticzUsername), value.ToString());
            }
        }

        public String DomoticzPassword
        {
            get
            {
                var password = "";
                var value = _helper.Read<string>(nameof(DomoticzPassword), password.ToString());
                return value;
            }
            set
            {
                _helper.Write(nameof(DomoticzPassword), value.ToString());
            }
        }
    }
}


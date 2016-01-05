using System;
using Windows.UI.Xaml;

namespace DomoticzUWP.ViewModels
{
    public class SettingsPageViewModel : DomoticzUWP.Mvvm.ViewModelBase
    {
        public SettingsPartViewModel SettingsPartViewModel { get; } = new SettingsPartViewModel();
        public AboutPartViewModel AboutPartViewModel { get; } = new AboutPartViewModel();
    }

    public class SettingsPartViewModel : Mvvm.ViewModelBase
    {
        Services.SettingsServices.SettingsService _settings;

        public SettingsPartViewModel()
        {
            if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                _settings = Services.SettingsServices.SettingsService.Instance;
        }

        private string _DomoticzUsernameText = "Domoticz Username";
        public string DomoticzUsernameText
        {
            get { return _DomoticzUsernameText; }
            set { Set(ref _DomoticzUsernameText, value); }
        }

        private string _DomoticzPasswordText = "Domoticz Password";
        public string DomoticzPasswordText
        {
            get { return _DomoticzPasswordText; }
            set { Set(ref _DomoticzPasswordText, value); }
        }

        private string _DomoticzUrlText = "Domoticz Host Url";
        public string DomoticzUrlText
        {
            get { return _DomoticzUrlText; }
            set { Set(ref _DomoticzUrlText, value); }
        }

        public void ShowBusy()
        {
            Views.Shell.SetBusy(true, _DomoticzUrlText);
        }

        public void HideBusy()
        {
            Views.Shell.SetBusy(false);
        }
    }

    public class AboutPartViewModel : Mvvm.ViewModelBase
    {
        public Uri Logo => Windows.ApplicationModel.Package.Current.Logo;

        public string DisplayName => Windows.ApplicationModel.Package.Current.DisplayName;

        public string Publisher => Windows.ApplicationModel.Package.Current.PublisherDisplayName;

        public string Version
        {
            get
            {
                var ver = Windows.ApplicationModel.Package.Current.Id.Version;
                return ver.Major.ToString() + "." + ver.Minor.ToString() + "." + ver.Build.ToString() + "." + ver.Revision.ToString();
            }
        }

        public Uri RateMe => new Uri("http://bing.com");
    }
}


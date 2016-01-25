using DomoticzUWP.Models;
using DomoticzUWP.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace DomoticzUWP.ViewModels
{    
    public class SettingsPageViewModel : DomoticzUWP.Mvvm.ViewModelBase
    {
        public SettingsPartViewModel SettingsPartViewModel { get; } = new SettingsPartViewModel();
        public AboutPartViewModel AboutPartViewModel { get; } = new AboutPartViewModel();
    }

    public class SettingsPartViewModel : Mvvm.ViewModelBase
    {
        private ObservableCollection<Device> _DevicesItems = new ObservableCollection<Device>();
        public ObservableCollection<Device> DevicesItems { get { return _DevicesItems; } set { Set(ref _DevicesItems, value); base.RaisePropertyChanged(); } }

        Services.SettingsServices.SettingsService _settings;

        public async Task AsyncTestConnection()
        {
            Boolean connection = await APIService.Instance.TestConnection();
            String message = "NO Connection! Maybe URL or username/password is incorrect.";
            if (connection)
            {
                message = "Connection is correct!";
            }

            var messageDialog = new MessageDialog(message);


            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 1;

            // Show the message dialog
            await messageDialog.ShowAsync();

        }

        public void TestConnection()
        {
            var test = AsyncTestConnection();
        }

        public SettingsPartViewModel()
        {
            if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                _settings = Services.SettingsServices.SettingsService.Instance;
        }

        public string DomoticzUsernameText
        {
            get { return _settings.DomoticzUsername; }
            set { _settings.DomoticzUsername = value; base.RaisePropertyChanged(); }
        }

        public string DomoticzPasswordText
        {
            get { return _settings.DomoticzPassword; }
            set { _settings.DomoticzPassword = value; base.RaisePropertyChanged(); }
        }

        public string DomoticzUrlText
        {
            get { return _settings.DomoticzHost; }
            set { _settings.DomoticzHost = value; base.RaisePropertyChanged(); }
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


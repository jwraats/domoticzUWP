using DomoticzUWP.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

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
        public SolidColorBrush ColorTest {
            get; set;
        }

        public async Task AsyncTestConnection()
        {
            ColorTest = new SolidColorBrush(Colors.Orange);
            Boolean connection = await APIService.GetInstance().TestConnection();
            if (connection)
            {
                ColorTest = new SolidColorBrush(Colors.Green);
            }
            else
            {
                ColorTest = new SolidColorBrush(Colors.Red);
            }
        }

        public void TestConnection()
        {
            ColorTest = new SolidColorBrush(Colors.Orange);
            var test = AsyncTestConnection();
        }

        public SettingsPartViewModel()
        {
            if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                _settings = Services.SettingsServices.SettingsService.Instance;

            ColorTest = new SolidColorBrush(Colors.LightGray);
        }

        public string DomoticzUsernameText
        {
            get { return _settings.DomoticzUsername; }
            set { _settings.DomoticzUsername = value; }
        }

        public string DomoticzPasswordText
        {
            get { return _settings.DomoticzPassword; }
            set { _settings.DomoticzPassword = value; }
        }

        public string DomoticzUrlText
        {
            get { return _settings.DomoticzHost; }
            set { _settings.DomoticzHost = value; }
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


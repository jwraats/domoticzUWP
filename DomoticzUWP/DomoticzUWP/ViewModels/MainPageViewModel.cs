using DomoticzUWP.Models;
using DomoticzUWP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace DomoticzUWP.ViewModels
{
    public class MainPageViewModel : Mvvm.ViewModelBase
    {
        private ImageSource _FloorplanSource = null;
        private Floorplan fp = null;
        public ImageSource FloorplanSource { get { return _FloorplanSource; } set { Set(ref _FloorplanSource, value); base.RaisePropertyChanged(); } }
        public double ImageWidth { get {  return ApplicationView.GetForCurrentView().VisibleBounds.Width; }   }
        private ObservableCollection<Device> _DevicesItems = new ObservableCollection<Device>();
        public ObservableCollection<Device> DevicesItems { get { return _DevicesItems; } set { Set(ref _DevicesItems, value); base.RaisePropertyChanged(); } }
        public MainPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                Value = "Designtime value";

            var loadFP = loadFloorplan(); 
        }


        public async Task loadFloorplan()
        {
            fp = await APIService.Instance.getFloorplan();
            if (fp != null && APIService.Instance.status)
            {
                FloorplanSource = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(APIService.Instance.apiURL + fp.Image));

                List<Device> devices = await APIService.Instance.getDevicesByFloor(fp);
                DevicesItems = new ObservableCollection<Device>();
                devices.ForEach(DevicesItems.Add);
                base.RaisePropertyChanged();
            }
            

        }

            string _Value = string.Empty;
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        public override void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (state.ContainsKey(nameof(Value)))
                Value = state[nameof(Value)]?.ToString();
            state.Clear();
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending)
        {
            if (suspending)
                state[nameof(Value)] = Value;
            await Task.Yield();
        }

        public void GotoSettings()
        {
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);
        }
    }
}


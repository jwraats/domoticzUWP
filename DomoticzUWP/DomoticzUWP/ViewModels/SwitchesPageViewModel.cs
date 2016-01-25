using DomoticzUWP.Models;
using DomoticzUWP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;

namespace DomoticzUWP.ViewModels
{
    public class SwitchesPageViewModel : DomoticzUWP.Mvvm.ViewModelBase
    {
        private ObservableCollection<Device> _DevicesItems = new ObservableCollection<Device>();
        public ObservableCollection<Device> DevicesItems { get { return _DevicesItems; } set { Set(ref _DevicesItems, value); base.RaisePropertyChanged(); } }

        public SwitchesPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                Value = "Designtime value";

            var lights = loadLights();
        }

        public async Task loadLights()
        {
            List<Device> devices = await APIService.Instance.getDevices("light");
            DevicesItems = new ObservableCollection<Device>();
            devices.ForEach(DevicesItems.Add);
        }

        private string _Value = string.Empty;
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

        public override void OnNavigatingFrom(NavigatingEventArgs args)
        {
            args.Cancel = false;
        }
    }
}


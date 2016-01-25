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
    public class UtilityPageViewModel : DomoticzUWP.Mvvm.ViewModelBase
    {
        private ObservableCollection<Device> _Utilities = new ObservableCollection<Device>();
        public ObservableCollection<Device> Utilities { get { return _Utilities; } set { Set(ref _Utilities, value); base.RaisePropertyChanged(); } }

        public UtilityPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                Value = "Designtime value";
            
            var utilities = loadUtilities();
        }

        public void reloadDevices(bool change)
        {
            var utilities = loadUtilities();
        }

        private async Task loadUtilities()
        {
            List<Device> utilities = await APIService.Instance.getDevices("utility");
            Utilities = new ObservableCollection<Device>();
            utilities.ForEach(delegate (Device d)
            {
                d.reloadDevices = reloadDevices;
                Utilities.Add(d);
            });
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


using DomoticzUWP.ViewModels;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls;

namespace DomoticzUWP.Views
{
    public sealed partial class UtilityPage : Page
    {
        public UtilityPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Disabled;
        }

        // strongly-typed view models enable x:bind
        public UtilityPageViewModel ViewModel => DataContext as UtilityPageViewModel;
    }
}


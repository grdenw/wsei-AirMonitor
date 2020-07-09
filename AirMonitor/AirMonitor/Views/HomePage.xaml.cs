using AirMonitor.Models;
using AirMonitor.ViewModels;
using Xamarin.Forms;

namespace AirMonitor.Views
{
    public partial class HomePage : ContentPage
    {
        private HomeViewModel ViewModel => BindingContext as HomeViewModel;

        public HomePage()
        {
            InitializeComponent();

            BindingContext = new HomeViewModel(Navigation);
        }

        private void ListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            ViewModel.GoToDetailsCommand.Execute(e.Item as Measurement);
        }
    }
}

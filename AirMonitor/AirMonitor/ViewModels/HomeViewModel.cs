using AirMonitor.HttpClients;
using AirMonitor.Models;
using AirMonitor.Views;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AirMonitor.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly INavigation navigation;
        private readonly AirlyHttpClient client;

        private bool isBusy;

        private List<Measurement> measurements;
        private ICommand goToDetailsCommand;


        public HomeViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            client = new AirlyHttpClient();
            Initialize();
        }

        public ICommand GoToDetailsCommand => goToDetailsCommand ?? (goToDetailsCommand = new Command<Measurement>(OnGoToDetails));

        public List<Measurement> Measurements
        {
            get => measurements;
            set => SetProperty(ref measurements, value);
        }

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        private void OnGoToDetails(Measurement measurement)
        {
            navigation.PushAsync(new DetailsPage(measurement));
        }

        private async Task Initialize()
        {
            IsBusy = true;

            var installations = await client.GetNearestInstallationsAsync();

            var measurementsList = new List<Measurement>();

            foreach (var nearestInstallation in installations)
            {
                measurementsList.Add(await client.GetMeasurementsByIdAsync(nearestInstallation));
            }

            Measurements = measurementsList;

            IsBusy = false;
        }
    }
}

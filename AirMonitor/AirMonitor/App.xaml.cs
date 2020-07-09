using AirMonitor.Views;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace AirMonitor
{
    public partial class App : Application
    {
        public static string AirlyApiKey { get; private set; }
        public static string AirlyApiUrl { get; private set; }
        public static string AirlyApiMeasurementUrl { get; private set; }
        public static string AirlyApiInstallationUrl { get; private set; }
        public App()
        {
            InitializeComponent();

            MainPage = new RootTabbedPage();
            LoadConfig();
        }

        private static void LoadConfig()
        {
            var assembly = Assembly.GetAssembly(typeof(App));
            var resourceNames = assembly.GetManifestResourceNames();
            var configName = resourceNames.FirstOrDefault(s => s.Contains("config.json"));

            using (var stream = assembly.GetManifestResourceStream(configName))
            {
                using (var reader = new StreamReader(stream ?? throw new InvalidOperationException()))
                {
                    var json = reader.ReadToEnd();
                    var jsonParsed = JObject.Parse(json);

                    AirlyApiKey = jsonParsed["AirlyApiKey"].Value<string>();
                    AirlyApiUrl = jsonParsed["AirlyApiUrl"].Value<string>();
                    AirlyApiMeasurementUrl = jsonParsed["AirlyApiMeasurementUrl"].Value<string>();
                    AirlyApiInstallationUrl = jsonParsed["AirlyApiInstallationUrl"].Value<string>();
                }
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

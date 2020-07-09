using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using AirMonitor.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace AirMonitor.HttpClients
{
    public class AirlyHttpClient
    {
        public async Task<NearestInstallation[]> GetNearestInstallationsAsync()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                var geoLocation = await Geolocation.GetLocationAsync(request).ConfigureAwait(false);

                var query = GetQuery(new Dictionary<string, object>
                {
                    {"lat", geoLocation.Latitude},
                    {"lng", geoLocation.Longitude},
                    {"maxDistanceKM", "1000"},
                    {"maxResults", "1"}
                });

                var requestUri = GetAirlyApiUrl(App.AirlyApiInstallationUrl, query);
                var result = await GetHttpResponseAsync<NearestInstallation[]>(requestUri);

                return result;

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        public async Task<Measurement> GetMeasurementsByIdAsync(NearestInstallation nearestInstallation)
        {
            try
            {
                var query = GetQuery(new Dictionary<string, object>
                {
                    { "installationId", nearestInstallation.Id }
                });

                var requestUri = GetAirlyApiUrl(App.AirlyApiMeasurementUrl, query);

                var result = await GetHttpResponseAsync<Measurement>(requestUri);

                result.NearestInstallation = nearestInstallation;
                result.CurrentDisplayValue = (int)Math.Round(result.Current?.Indexes?.FirstOrDefault()?.Value ?? 0);

                return result;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        private static void PrepareHttpClient(HttpClient client)
        {
            client.BaseAddress = new Uri(App.AirlyApiUrl);

            client.DefaultRequestHeaders
                .Add("apikey", App.AirlyApiKey);

            client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private static async Task<T> GetHttpResponseAsync<T>(string url)
        {

            using (var client = new HttpClient())
            {
                try
                {
                    PrepareHttpClient(client);

                    var response = await client.GetAsync(url);

                    if (response.Headers.TryGetValues("X-RateLimit-Limit-day", out var dayLimit) &&
                        response.Headers.TryGetValues("X-RateLimit-Remaining-day", out var dayLimitRemaining))
                    {
                        System.Diagnostics.Debug.WriteLine(
                            $"Day limit: {dayLimit?.FirstOrDefault()}, remaining: {dayLimitRemaining?.FirstOrDefault()}");
                    }

                    switch ((int)response.StatusCode)
                    {
                        case 200:
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<T>(content);
                            return result;
                        case 429:
                            System.Diagnostics.Debug.WriteLine("Too many requests");
                            break;
                        default:
                            var errorContent = await response.Content.ReadAsStringAsync();
                            System.Diagnostics.Debug.WriteLine($"Response error: {errorContent}");
                            return default;
                    }
                }
                catch (JsonReaderException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
                catch (WebException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }

                return default;
            }
        }

        private static string GetQuery(IDictionary<string, object> args)
        {
            if (args == null) return null;

            var query = HttpUtility.ParseQueryString(string.Empty);

            foreach (var arg in args)
            {
                if (arg.Value is double number)
                {
                    query[arg.Key] = number.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    query[arg.Key] = arg.Value?.ToString();
                }
            }

            return query.ToString();
        }

        private static string GetAirlyApiUrl(string path, string query)
        {
            var builder = new UriBuilder(App.AirlyApiUrl) {Port = -1};
            builder.Path += path;
            builder.Query = query;
            var url = builder.ToString();

            return url;
        }
    }
}




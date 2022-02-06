using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Model;
using WeatherApp.Utility;
using Xamarin.Essentials;

namespace WeatherApp.Services
{
    public class WeatherService : IWeatherService
    {
        public async Task<WeatherRoot> GetWeatherAsync(double latitude, double longitude)
        {
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                throw (new Exception(Constants.NetworkError));
            }

            using (var client = new HttpClient())
            {
                string json = string.Empty;
                var url = string.Format(Constants.WeatherURl, latitude, longitude);
                var response = await client?.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    json = await response?.Content?.ReadAsStringAsync();
                }
                if (string.IsNullOrWhiteSpace(json))
                {
                    return null;
                }
                return JsonConvert.DeserializeObject<WeatherRoot>(json);
            }
        }
    }
}

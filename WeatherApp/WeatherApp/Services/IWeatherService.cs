using System.Threading.Tasks;
using WeatherApp.Model;

namespace WeatherApp.Services
{
    //Interface to get the weather details from api
    public interface IWeatherService
    {
        //Get weather details for particular city
        Task<WeatherRoot> GetWeatherAsync(double latitude, double longitude);
    }
}

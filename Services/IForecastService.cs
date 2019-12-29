using System.Collections.Generic;
using System.Threading.Tasks;
using MyPlayground.DBContext;
using MyPlayground.Models;
using Newtonsoft.Json.Linq;

namespace MyPlayground.Services
{
    public interface IForecastService
    {
        Task<Forecast> GetWeatherForecasts(double latitude, double longitude);
        Forecast MapWeatherForecasts(JObject forecasts);
        IEnumerable<CitiesList> GetCities(string city);
    }
}
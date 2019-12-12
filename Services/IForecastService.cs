using System.Collections.Generic;
using System.Threading.Tasks;
using MyPlayground.Models;
using Newtonsoft.Json.Linq;

namespace MyPlayground.Services
{
    public interface IForecastService
    {
        Task<IEnumerable<Forecast>> GetWeatherForecasts(double latitude, double longitude);
        IEnumerable<Forecast> MapWeatherForecasts(JObject forecasts);
    }
}
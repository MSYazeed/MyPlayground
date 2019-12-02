using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPlayground.Services;

namespace MyPlayground.Controllers
{
    [Route("api/forecast")]
    public class WeatherForecastController : Controller
    {
        [HttpGet("[action]")]
        public async Task<IEnumerable<WeatherForecast>> WeatherForecasts()
        {
            var weatherForecastService = new WeatherForecastService();
            return await weatherForecastService.GetWeatherForecasts();
        }
    }
}
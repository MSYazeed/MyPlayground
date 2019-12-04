using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPlayground.Models;
using MyPlayground.Services;

namespace MyPlayground.Controllers
{
    [Route("api/forecast")]
    public class ForecastController : Controller
    {
        public ForecastController()
        {

        }

        [HttpGet]
        [Route("search/{latitude},{longitude}")]
        public async Task<IEnumerable<Forecast>> WeatherForecasts(double latitude, double longitude)
        {
            var weatherForecastService = new ForecastService();
            return await weatherForecastService.GetWeatherForecasts(latitude, longitude);
        }
    }
}
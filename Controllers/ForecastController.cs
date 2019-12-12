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
        private readonly IForecastService _forecastService;

        public ForecastController(IForecastService forecastService)
        {
            _forecastService = forecastService;
        }

        [HttpGet]
        [Route("search/{latitude},{longitude}")]
        public async Task<IEnumerable<Forecast>> WeatherForecasts(/*[FromServices] IForecastService forecastService,*/ double latitude, double longitude)
        {
            return await _forecastService.GetWeatherForecasts(latitude, longitude);
        }
    }
}
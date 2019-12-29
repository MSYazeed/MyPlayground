using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPlayground.DBContext;
using MyPlayground.Models;
using MyPlayground.Services;

namespace MyPlayground.Controllers
{
    [Route("api")]
    public class ForecastController : Controller
    {
        private readonly IForecastService _forecastService;

        public ForecastController(IForecastService forecastService)
        {
            _forecastService = forecastService;
        }

        [HttpGet]
        [Route("forecast/search/{latitude},{longitude}")]
        public async Task<Forecast> WeatherForecasts(/*[FromServices] IForecastService forecastService,*/ double latitude, double longitude)
        {
            return await _forecastService.GetWeatherForecasts(latitude, longitude);
        }

        [HttpGet]
        [Route("city/search/{city}")]
        public IEnumerable<CitiesList> GetCities(/*[FromServices] IForecastService forecastService,*/ string city)
        {

            return _forecastService.GetCities(city);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyPlayground.Controllers
{
    [Route("api/forecast")]
    public class ForecastController : Controller
    {
        private static readonly string[] Summaries =
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public async Task<IEnumerable<WeatherForecast>> WeatherForecasts()
        {
            var secretKey = "1278a4bf12113ed647b1a64f13f354c6";

            var baseUrl = "https://api.darksky.net/forecast/";

            var location = "48.1351,11.5820";

            var exclusions = "exclude=[minutely,hourly,flags]";

            var apiUrl = baseUrl + secretKey + "/" + location + "?" + exclusions + "&units=si";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(apiUrl);
                var content = JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync());
                return MapWeatherForecasts(content);
            }
        }

        private IEnumerable<WeatherForecast> MapWeatherForecasts(JObject forecasts)
        {
            var forecastList = new List<WeatherForecast>();
            var dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            foreach (var forecast in forecasts["daily"]["data"])
            {
                forecastList.Add(new WeatherForecast
                {
                    TemperatureCMax = forecast["temperatureMax"].Value<int>(),
                    TemperatureCMin = forecast["temperatureMin"].Value<int>(),
                    Summary = forecast["summary"].Value<string>(),
                    DateFormatted = dateTime.AddSeconds(forecast["time"].Value<double>()).ToString("d")
                });
            }

            return forecastList;
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public int TemperatureCMax { get; set; }
            public int TemperatureCMin { get; set; }
            public string Summary { get; set; }
        }
    }
}
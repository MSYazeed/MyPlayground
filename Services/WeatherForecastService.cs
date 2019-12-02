using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MyPlayground.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyPlayground.Services
{
    public class WeatherForecastService
    {
        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecasts()
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
    }
}

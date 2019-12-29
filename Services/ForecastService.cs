using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MyPlayground.DBContext;
using MyPlayground.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyPlayground.Services
{
    public class ForecastService : IForecastService
    {
        public async Task<Forecast> GetWeatherForecasts(double latitude, double longitude)
        {
            var secretKey = "1278a4bf12113ed647b1a64f13f354c6";

            var baseUrl = "https://api.darksky.net/forecast/";

            var location = $"{latitude},{longitude}";

            var exclusions = "exclude=[minutely,hourly,flags]";

            var apiUrl = baseUrl + secretKey + "/" + location + "?" + exclusions + "&units=si";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(apiUrl);
                var content = JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync());
                return MapWeatherForecasts(content);
            }
        }

        public Forecast MapWeatherForecasts(JObject forecasts)
        {
            var forecastInfo = new Forecast();
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            forecastInfo.CurrentDateTimeFormatted = dateTime.AddSeconds(forecasts["currently"]["time"].Value<int>())
                .ToString("dddd hh:mm");
            forecastInfo.CurrentTemperatureC = forecasts["currently"]["temperature"].Value<double>();
            forecastInfo.CurrentHumidity = forecasts["currently"]["humidity"].Value<double>();
            forecastInfo.CurrentWindSpeed = forecasts["currently"]["windSpeed"].Value<double>();
            forecastInfo.CurrentSummary = forecasts["currently"]["summary"].Value<string>();
            forecastInfo.CurrentCondition = forecasts["currently"]["icon"].Value<string>();

            foreach (var forecast in forecasts["daily"]["data"])
                forecastInfo.DaysForecast.Add(new DaysForecast
                {
                    TemperatureCMax = forecast["temperatureMax"].Value<int>(),
                    TemperatureCMin = forecast["temperatureMin"].Value<int>(),
                    Summary = forecast["summary"].Value<string>(),
                    Day = dateTime.AddSeconds(forecast["time"].Value<double>()).ToString("dddd"),
                    Condition = forecast["icon"].Value<string>()
                });

            return forecastInfo;
        }

        public IEnumerable<CitiesList> GetCities(string city)
        {
            using (var db =
                new MyPlaygroundDbContext(
                    "server=(LocalDb)\\MSSQLLocalDB;database=MyPlayground;trusted_connection=true;"))
            {
                var citiesList = db.CitiesList.Where(list => list.Name.Contains(city)).ToList();
                return citiesList.Take(5);
            }
        }
    }
}
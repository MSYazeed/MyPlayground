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
        public async Task<IEnumerable<Forecast>> GetWeatherForecasts(double latitude, double longitude)
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

        public IEnumerable<Forecast> MapWeatherForecasts(JObject forecasts)
        {
            var forecastList = new List<Forecast>();
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            foreach (var forecast in forecasts["daily"]["data"])
                forecastList.Add(new Forecast
                {
                    TemperatureCMax = forecast["temperatureMax"].Value<int>(),
                    TemperatureCMin = forecast["temperatureMin"].Value<int>(),
                    Summary = forecast["summary"].Value<string>(),
                    DateFormatted = dateTime.AddSeconds(forecast["time"].Value<double>()).ToString("d")
                });

            return forecastList;
        }

        public IEnumerable<CitiesList> GetCities(string city)
        {
            using (var db = new MyPlaygroundDbContext("server=(LocalDb)\\MSSQLLocalDB;database=MyPlayground;trusted_connection=true;"))
            {
                var citiesList = db.CitiesList.Where(list => list.Name.Contains(city)).ToList();
                return citiesList.Take(5);
            }
        }
    }
}
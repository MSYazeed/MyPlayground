using System.Collections.Generic;

namespace MyPlayground.Models
{
    public class Forecast
    {
        public Forecast()
        {
            DaysForecast = new List<DaysForecast>();
        }

        public string CurrentDateTimeFormatted { get; set; }
        public double CurrentTemperatureC { get; set; }
        public double CurrentHumidity { get; set; }
        public double CurrentWindSpeed { get; set; }
        public string CurrentSummary { get; set; }
        public string CurrentCondition { get; set; }
        public List<DaysForecast> DaysForecast { get; set; }
    }

    public class DaysForecast
    {
        public string Day { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureCMax { get; set; }
        public int TemperatureCMin { get; set; }
        public string Summary { get; set; }
        public string Condition { get; set; }
    }
}

using System;

namespace WeatherLib.Forecasts
{
    public class Forecast
    {
        public string CityName { get; set; }
        public string TemperatureMax { get; set; }
        public string TemperatureMin { get; set; }
        public DateTime LastUpdate { get; set; }

        public override string ToString()
        {
            return $"{CityName}: от {TemperatureMin}℃ до {TemperatureMax}℃\n"
                 + $"Последнее обновление {LastUpdate}";
        }
    }
}

using System;
using System.Collections.Generic;
using WeatherLib.Forecasts;
using WeatherLib.Services;

namespace WeatherLib.Providers
{
    public class WeatherProvider : IWeatherProvider
    {
        private Dictionary<string, Forecast> cache;
        private IWeatherService service;

        public WeatherProvider(IWeatherService service)
        {
            this.service = service;
            cache = new();
        }

        public Forecast Get(string cityName)
        {
            cityName = NormalizeCityName(cityName);

            if (cache.ContainsKey(cityName))
            {
                if (IsActual(cache[cityName]))
                    return cache[cityName];
            }

            Forecast forecast = service.Get(cityName);

            if (forecast.GetType() != typeof(NullForecast))
                cache[cityName] = forecast;

            return forecast;
        }

        public void SetService(IWeatherService service)
        {
            this.service = service;
        }

        public string GetCurrentServiceName()
        {
            return service.GetServiceName();
        }

        private bool IsActual(Forecast forecast)
        {
            TimeSpan span = DateTime.UtcNow.Subtract(forecast.LastUpdate);
            return span.Hours < 1;
        }

        private string NormalizeCityName(string cityName)
        {
            cityName = cityName.ToLower();
            cityName = cityName[0].ToString().ToUpper() + cityName.Substring(1);

            return cityName;
        }
    }
}

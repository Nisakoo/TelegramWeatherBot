using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Xml.Linq;
using WeatherLib.Forecasts;

namespace WeatherLib.Services
{
    public class MeteoService : IWeatherService
    {
        private HttpClient client;
        private Dictionary<string, string> urls;

        public MeteoService()
        {
            client = new();
            urls = GetTowns();
        }

        public Forecast Get(string cityName)
        {
            cityName = NormalizeCityName(cityName);

            if (!urls.ContainsKey(cityName))
                return new NullForecast();

            string xmlData = client.GetStringAsync(urls[cityName]).Result;

            XElement[] forecasts = XDocument.Parse(xmlData)
                            .Descendants("MMWEATHER")
                            .Descendants("REPORT")
                            .Descendants("TOWN")
                            .Descendants("FORECAST")
                            .ToArray();

            XElement forecast = forecasts[0];

            return new Forecast
            {
                CityName = cityName,
                TemperatureMax = forecast.Element("TEMPERATURE").Attribute("max").Value,
                TemperatureMin = forecast.Element("TEMPERATURE").Attribute("min").Value,
                LastUpdate = DateTime.UtcNow
            };
        }

        public string GetServiceName()
        {
            return "MeteoService www.meteoservice.ru";
        }

        private Dictionary<string, string> GetTowns()
        {
            string jsonData = File.ReadAllText(@"./towns.json");
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);
        }

        private string NormalizeCityName(string cityName)
        {
            cityName = cityName.ToLower();
            cityName = cityName[0].ToString().ToUpper() + cityName.Substring(1);

            return cityName;
        }
    }
}

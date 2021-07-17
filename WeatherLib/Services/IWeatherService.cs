using WeatherLib.Forecasts;

namespace WeatherLib.Services
{
    public interface IWeatherService
    {
        public Forecast Get(string cityName);
        public string GetServiceName();
    }
}

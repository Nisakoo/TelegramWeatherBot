using WeatherLib.Forecasts;
using WeatherLib.Services;

namespace WeatherLib.Providers
{
    public interface IWeatherProvider
    {
        public Forecast Get(string cityName);
        public void SetService(IWeatherService service);
        public string GetCurrentServiceName();
    }
}

namespace WeatherLib.Forecasts
{
    public class NullForecast : Forecast
    {
        public override string ToString()
        {
            return "Извините, я не знаю такого города";
        }
    }
}

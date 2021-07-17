using System.IO;
using System.Text.Json;
using TelegramBot;
using WeatherLib.Providers;
using WeatherLib.Services;

namespace TelegramWeatherBot
{
    class Program
    {
        private static IWeatherProvider provider;
        private static WeatherBot bot;
        
        static void Main(string[] args)
        {
            IWeatherService service = new MeteoService();
            provider = new WeatherProvider(service);

            bot = new(GetTokenFromConfig(), provider);
        }

        private static string GetTokenFromConfig()
        {
            JsonDocument json = JsonDocument.Parse(File.ReadAllText(@"./config.json"));
            JsonElement root = json.RootElement;

            string token = root.GetProperty("Token").GetString();

            return token;
        }
    }
}

using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using WeatherLib.Forecasts;
using WeatherLib.Providers;

namespace TelegramBot
{
    public class WeatherBot
    {
        private TelegramBotClient client;
        private IWeatherProvider provider;

        public WeatherBot(string token, IWeatherProvider provider)
        {
            this.provider = provider;
            client = new TelegramBotClient(token);

            client.OnMessage += Client_OnMessage;
        }

        public void Start()
        {
            client.StartReceiving();
            Console.ReadLine();
        }

        private void Client_OnMessage(object sender, MessageEventArgs args)
        {
            string messageText = args.Message.Text;
            string response = "";

            if (messageText == "/service")
            {
                response += provider.GetCurrentServiceName();
            }
            else
            {
                Forecast now = provider.Get(messageText);
                response += now;
            }

            client.SendTextMessageAsync(args.Message.Chat.Id, response);
        }
    }
}

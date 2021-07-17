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

            client.StartReceiving();
            Console.ReadLine();
        }

        private void Client_OnMessage(object sender, MessageEventArgs args)
        {
            string messageText = args.Message.Text;
            Forecast now = provider.Get(messageText);

            client.SendTextMessageAsync(args.Message.Chat.Id, now.ToString());
        }
    }
}

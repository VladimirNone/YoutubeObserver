using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Telegram.Bot;
using YoutubeTelegramBot.Infrastructure.Telegram.Implementations.Commands;
using YoutubeTelegramBot.Infrastructure.Telegram.Interfaces;

namespace YoutubeTelegramBot.Infrastructure.Telegram.Implementations
{
    public class BotService : IBotService
    {
        private List<Command> commands { get; set; } = new List<Command>();

        public BotService(IConfiguration configuration)
        {
            Client = new TelegramBotClient(configuration["TelegramBotToken"]);

            commands.Add(new HelloCommand(this));

        }

        public Command GetCommand(string name)
        {
            return commands.Find(h => h.Name == name);
        }

        public TelegramBotClient Client { get; }
    }
}

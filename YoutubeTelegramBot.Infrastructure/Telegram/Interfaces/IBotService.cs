
using Telegram.Bot;

namespace YoutubeTelegramBot.Infrastructure.Telegram.Interfaces
{
    public interface IBotService
    {
        TelegramBotClient Client { get; }

        Command GetCommand(string name);
    }
}
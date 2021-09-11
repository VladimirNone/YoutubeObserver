
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using YoutubeTelegramBot.Domain.POCOs;

namespace YoutubeTelegramBot.Infrastructure.Telegram.Interfaces
{
    public interface IBotService
    {
        List<Command> Commands { get; }
        TelegramBotClient Client { get; }

        Command GetCommand(string name);
        Task ShowVideos(List<Video> video);
    }
}
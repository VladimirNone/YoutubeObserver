using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using YoutubeTelegramBot.Infrastructure.Telegram.Interfaces;
using YoutubeTelegramBot.Infrastructure.Youtube.Interfaces;
using YoutubeTelegramBot.Repositories.Interfaces;

namespace YoutubeTelegramBot.Infrastructure.Telegram.Implementations.Commands
{
    public class HelloCommand : Command
    {
        public override string Name => "/hello";

        public HelloCommand(IBotService botService, IYoutubeService youtubeService, IUnitOfWork unitOfWork)
            :base(botService, youtubeService, unitOfWork)
        { 
        }

        public override async Task ExecuteAsync(Update update)
        {
            var message = update.Message;

            await botService.Client.SendTextMessageAsync(message.Chat.Id, "hello bro");
        }
    }
}

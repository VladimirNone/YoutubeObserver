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
    public class ShowCommandsCommand : Command
    {
        public override string Name => "/showCommands";

        public override string Description => "Показать команды и их описание";

        public ShowCommandsCommand(IBotService botService, IYoutubeService youtubeService, IUnitOfWork unitOfWork)
            : base(botService, youtubeService, unitOfWork)
        {
        }

        public override async Task ExecuteAsync(Update update)
        {
            var message = update.Message;

            var answerBody = "";
            foreach (var item in botService.Commands)
            {
                answerBody += item.Name + " - " + item.Description + "\n";
            }

            await botService.Client.SendTextMessageAsync(message.Chat.Id, answerBody);
        }
    }
}

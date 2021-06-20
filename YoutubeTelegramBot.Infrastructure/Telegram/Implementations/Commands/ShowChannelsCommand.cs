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
    public class ShowChannelsCommand : Command
    {
        public override string Name => "/showChannels";

        public ShowChannelsCommand(IBotService botService, IYoutubeService youtubeService, IUnitOfWork unitOfWork)
            : base(botService, youtubeService, unitOfWork)
        {
        }

        public override async Task ExecuteAsync(Update update)
        {
            var message = update.Message;

            var listOfChannels = await unitOfWork.ChannelsRepository.GetChannelsAsync();

            var startPartChannelUrl = @"https://www.youtube.com/channel/";

            var answer = "";

            foreach (var item in listOfChannels)
            {
                answer += $"{item.name}\n" +
                    $"Последняя проверка: {item.last_check}\n" +
                    $"Ссылка: {startPartChannelUrl}{item.youtube_id}\n";
            }

            await botService.Client.SendTextMessageAsync(message.Chat.Id, answer);
        }
    }
}

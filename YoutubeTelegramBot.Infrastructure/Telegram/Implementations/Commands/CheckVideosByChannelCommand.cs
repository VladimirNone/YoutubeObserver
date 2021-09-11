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
    public class CheckVideosByChannelCommand : Command
    {
        public override string Name => "/checkChannel";

        public override string Description => "Делает внеплановую проверку контента канала";

        public CheckVideosByChannelCommand(IBotService botService, IYoutubeService youtubeService, IUnitOfWork unitOfWork)
            : base(botService, youtubeService, unitOfWork)
        {
        }

        public override async Task ExecuteAsync(Update update)
        {
            var message = update.Message;

            if (string.IsNullOrEmpty(inputedData))
            {
                await botService.Client.SendTextMessageAsync(message.Chat.Id, $"Need to point out a name of channel");
                return;
            }

            var channel = await unitOfWork.ChannelsRepository.GetChannelByNameAsync(inputedData);

            if(channel == null)
            {
                await botService.Client.SendTextMessageAsync(message.Chat.Id, $"Wrong name of channel");
                return;
            }

            var newVideos = await youtubeService.SearchVideosAsync(channel);
            unitOfWork.ChannelsRepository.MarkNewCheckChannel(channel, newVideos);

            await unitOfWork.Commit();

            await botService.ShowVideos(channel.Videos);

            await botService.Client.SendTextMessageAsync(message.Chat.Id, $"Channel with name '{channel.name}' was checked");
        }
    }
}

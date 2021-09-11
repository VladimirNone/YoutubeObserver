using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using YoutubeTelegramBot.Domain.POCOs;
using YoutubeTelegramBot.Infrastructure.Telegram.Interfaces;
using YoutubeTelegramBot.Infrastructure.Youtube.Interfaces;
using YoutubeTelegramBot.Repositories.Interfaces;

namespace YoutubeTelegramBot.Infrastructure.Telegram.Implementations.Commands
{
    public class ShowDeferredVideosCommand : Command
    {
        public override string Name => "/showDeferredVideos";

        public override string Description => "Выводит отложенные видео";

        public ShowDeferredVideosCommand(IBotService botService, IYoutubeService youtubeService, IUnitOfWork unitOfWork)
            : base(botService, youtubeService, unitOfWork)
        {
        }

        public override async Task ExecuteAsync(Update update)
        {
            var defereedVideos = await unitOfWork.VideosRepository.GetVideosAsync(VideoStatus.Deferred);

            await botService.ShowVideos(defereedVideos);
        }
    }
}

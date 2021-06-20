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
    public class VideoNotInterestedCallbackCommand : Command
    {
        public override string Name => "/notInterested";

        public VideoNotInterestedCallbackCommand(IBotService botService, IYoutubeService youtubeService, IUnitOfWork unitOfWork)
            : base(botService, youtubeService, unitOfWork)
        {
        }

        public override async Task ExecuteAsync(Update update)
        {
            var callbackQuery = update.CallbackQuery;

            var videoId = callbackQuery.Message.Text.TrimStart(IYoutubeService.StartPartOfVideoUrl.ToCharArray());

            var video = await unitOfWork.VideosRepository.GetEntityAsync(videoId);
            video.not_interest = true;

            await unitOfWork.Commit();

            await botService.Client.DeleteMessageAsync(callbackQuery.Message.Chat.Id, callbackQuery.Message.MessageId);
        }
    }
}

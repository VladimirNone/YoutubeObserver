using Microsoft.Extensions.Logging;
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
    public class VideoNotInterestedCallbackCommand : Command
    {
        private readonly ILogger Logger;

        public override string Name => "/notInterested";

        public override string Description => "Колбэк для кнопки \"Не интересно\"";

        public VideoNotInterestedCallbackCommand(IBotService botService, IYoutubeService youtubeService, IUnitOfWork unitOfWork, ILogger logger)
            : base(botService, youtubeService, unitOfWork)
        {
            Logger = logger;
        }

        public override async Task ExecuteAsync(Update update)
        {
            var callbackQuery = update.CallbackQuery;

            var videoId = callbackQuery.Message.Text.TrimStart(IYoutubeService.StartPartOfVideoUrl.ToCharArray());

            var video = await unitOfWork.VideosRepository.GetEntityAsync(videoId);
            if (video != null)
            {
                video.status = VideoStatus.NotInterested;

                await unitOfWork.Commit();
            }

            try
            {
                await botService.Client.DeleteMessageAsync(callbackQuery.Message.Chat.Id, callbackQuery.Message.MessageId);
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex, "Deleting the message which were deleted");
            }
        }
    }
}

using Microsoft.Extensions.Logging;
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
    class AddVideoCommand : Command
    {
        private readonly ILogger Logger;

        public override string Name => "/addVideo";

        public AddVideoCommand(IBotService botService, IYoutubeService youtubeService, IUnitOfWork unitOfWork, ILogger logger)
            : base(botService, youtubeService, unitOfWork)
        {
            Logger = logger;
        }

        public override async Task ExecuteAsync(Update update)
        {
            var message = update.Message;

            if (string.IsNullOrEmpty(inputedData))
            {
                await botService.Client.SendTextMessageAsync(message.Chat.Id, $"Need to point out a url of video");
                return;
            }

            var youtubeVideoId = inputedData.Split('=')[1];

            try
            {
                var video = await youtubeService.SearchVideoAsync(youtubeVideoId);
                if (await unitOfWork.VideosRepository.VideoTracked(video.youtube_id))
                {
                    await botService.Client.SendTextMessageAsync(message.Chat.Id, $"Video with name '{video.name}' has already been added");
                    return;
                }

                await unitOfWork.VideosRepository.AddEntityAsync(video);
                await unitOfWork.Commit();

                await botService.ShowVideos(new[] { video }.ToList());
            }
            catch(Exception ex)
            {
                await unitOfWork.Rollback();
                await botService.Client.SendTextMessageAsync(message.Chat.Id, $"Video wasn't added");
                Logger.LogError(ex.Message);
            }
        }
    }
}

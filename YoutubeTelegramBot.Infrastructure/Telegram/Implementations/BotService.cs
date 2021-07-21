using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using YoutubeTelegramBot.Domain.POCOs;
using YoutubeTelegramBot.Infrastructure.Telegram.Implementations.Commands;
using YoutubeTelegramBot.Infrastructure.Telegram.Interfaces;
using YoutubeTelegramBot.Infrastructure.Youtube.Interfaces;
using YoutubeTelegramBot.Repositories.Interfaces;

namespace YoutubeTelegramBot.Infrastructure.Telegram.Implementations
{
    public class BotService : IBotService
    {
        private List<Command> commands { get; set; } = new List<Command>();

        public TelegramBotClient Client { get; }

        public BotService(IConfiguration configuration, IYoutubeService youtubeService, IUnitOfWork unitOfWork, ILogger<BotService> logger)
        {
            Client = new TelegramBotClient(configuration["TelegramBotToken"]);

            commands.Add(new HelloCommand(this, youtubeService, unitOfWork));
            commands.Add(new AddChannelCommand(this, youtubeService, unitOfWork));
            commands.Add(new AddVideoCommand(this, youtubeService, unitOfWork, logger));
            commands.Add(new CheckVideosByChannelCommand(this, youtubeService, unitOfWork));
            commands.Add(new ShowChannelsCommand(this, youtubeService, unitOfWork));
            commands.Add(new ShowDeferredVideosCommand(this, youtubeService, unitOfWork));
            commands.Add(new VideoWatchedCallbackCommand(this, youtubeService, unitOfWork, logger));
            commands.Add(new VideoDeferCallbackCommand(this, youtubeService, unitOfWork, logger));
            commands.Add(new VideoNotInterestedCallbackCommand(this, youtubeService, unitOfWork, logger));

        }

        public Command GetCommand(string name)
        {
            return commands.Find(h => h.Name == name);
        }

        public async Task ShowVideos(List<Video> videos)
        {
            if (videos.Count == 0 || videos == null)
                return;

            var command = new ShowVideoSystemCommand(this);

            command.showVideos = videos;

            await command.ExecuteAsync(null);
        }

    }
}

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
        public List<Command> Commands { get; private set; } = new List<Command>();

        public TelegramBotClient Client { get; }

        public BotService(IConfiguration configuration, IYoutubeService youtubeService, IUnitOfWork unitOfWork, ILogger<BotService> logger)
        {
            Client = new TelegramBotClient(configuration["TelegramBotToken"]);

            Commands.Add(new HelloCommand(this, youtubeService, unitOfWork));
            Commands.Add(new AddChannelCommand(this, youtubeService, unitOfWork));
            Commands.Add(new AddVideoCommand(this, youtubeService, unitOfWork, logger));
            Commands.Add(new CheckVideosByChannelCommand(this, youtubeService, unitOfWork));
            Commands.Add(new ShowCommandsCommand(this, youtubeService, unitOfWork));
            Commands.Add(new ShowChannelsCommand(this, youtubeService, unitOfWork));
            Commands.Add(new ShowDeferredVideosCommand(this, youtubeService, unitOfWork));
            Commands.Add(new VideoWatchedCallbackCommand(this, youtubeService, unitOfWork, logger));
            Commands.Add(new VideoDeferCallbackCommand(this, youtubeService, unitOfWork, logger));
            Commands.Add(new VideoNotInterestedCallbackCommand(this, youtubeService, unitOfWork, logger));

        }

        public Command GetCommand(string name)
        {
            return Commands.Find(h => h.Name == name);
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

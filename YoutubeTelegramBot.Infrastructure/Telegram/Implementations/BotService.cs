using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Telegram.Bot;
using YoutubeTelegramBot.Infrastructure.Telegram.Implementations.Commands;
using YoutubeTelegramBot.Infrastructure.Telegram.Interfaces;
using YoutubeTelegramBot.Infrastructure.Youtube.Interfaces;
using YoutubeTelegramBot.Repositories.Interfaces;

namespace YoutubeTelegramBot.Infrastructure.Telegram.Implementations
{
    public class BotService : IBotService
    {
        private List<Command> commands { get; set; } = new List<Command>();

        public BotService(IConfiguration configuration, IYoutubeService youtubeService, IUnitOfWork unitOfWork, ILogger<BotService> logger)
        {
            Client = new TelegramBotClient(configuration["TelegramBotToken"]);

            commands.Add(new HelloCommand(this, youtubeService, unitOfWork));
            commands.Add(new AddChannelCommand(this, youtubeService, unitOfWork));
            commands.Add(new ShowChannelsCommand(this, youtubeService, unitOfWork));
            commands.Add(new VideoWatchedCallbackCommand(this, youtubeService, unitOfWork, logger));
            commands.Add(new VideoNotInterestedCallbackCommand(this, youtubeService, unitOfWork, logger));

        }

        public Command GetCommand(string name)
        {
            return commands.Find(h => h.Name == name);
        }

        public TelegramBotClient Client { get; }
    }
}

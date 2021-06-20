using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using YoutubeTelegramBot.Infrastructure.Youtube.Interfaces;
using YoutubeTelegramBot.Repositories.Interfaces;

namespace YoutubeTelegramBot.Infrastructure.Telegram.Interfaces
{
    public abstract class Command
    {
        public abstract string Name { get; }
        public string inputedData { get; set; }
        public IBotService botService { get; set; }
        public IYoutubeService youtubeService { get; set; }
        public IUnitOfWork unitOfWork { get; set; }

        public static long ChatId { get; set; } = 597051235;

        public Command()
        {

        }

        public Command(IBotService botService, IYoutubeService youtubeService, IUnitOfWork unitOfWork)
        {
            this.botService = botService;
            this.youtubeService = youtubeService;
            this.unitOfWork = unitOfWork;
        }

        public abstract Task ExecuteAsync(Update update);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace YoutubeTelegramBot.Infrastructure.Telegram.Interfaces
{
    public abstract class Command
    {
        public abstract string Name { get; }
        public IBotService bot { get; set; }

        public Command(IBotService botService)
        {
            bot = botService;
        }

        public abstract Task Execute(Message message);
    }
}

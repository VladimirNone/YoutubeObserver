using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using YoutubeTelegramBot.Infrastructure.Telegram.Interfaces;

namespace YoutubeTelegramBot.Infrastructure.Telegram.Implementations.Commands
{
    public class HelloCommand : Command
    {
        public override string Name => "hello";

        public HelloCommand(IBotService botService)
            :base(botService)
        { 
        }

        public override async Task Execute(Message message)
        {
            await bot.Client.SendTextMessageAsync(message.Chat.Id, "hello bro");
        }
    }
}

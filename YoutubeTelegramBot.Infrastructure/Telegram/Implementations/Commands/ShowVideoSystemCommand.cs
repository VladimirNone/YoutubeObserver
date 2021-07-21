using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using YoutubeTelegramBot.Infrastructure.Telegram.Interfaces;
using Video = YoutubeTelegramBot.Domain.POCOs.Video;

namespace YoutubeTelegramBot.Infrastructure.Telegram.Implementations.Commands
{
    public class ShowVideoSystemCommand : Command
    {
        public override string Name => "";

        public List<Video> showVideos { get; set; }

        public ShowVideoSystemCommand(IBotService botService)
        {
            this.botService = botService;
        }

        public override async Task ExecuteAsync(Update update)
        {
            if (ChatId == 0)
                throw new Exception("ChatId weren't setted");

            var keyboard = new InlineKeyboardMarkup(new[] {
                        InlineKeyboardButton.WithCallbackData("Просмотрено", "/wasWatched"),
                        InlineKeyboardButton.WithCallbackData("Отложить", "/defer"),
                        InlineKeyboardButton.WithCallbackData("Не интересно", "/notInterested") });

            foreach (var video in showVideos)
            {
                await botService.Client.SendTextMessageAsync(ChatId, video.url, replyMarkup: keyboard);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using YoutubeTelegramBot.Infrastructure;
using YoutubeTelegramBot.Infrastructure.Telegram.Implementations.Commands;
using YoutubeTelegramBot.Infrastructure.Telegram.Interfaces;
using YoutubeTelegramBot.Infrastructure.Youtube.Interfaces;
using YoutubeTelegramBot.Repositories.Interfaces;
using Video = YoutubeTelegramBot.Domain.POCOs.Video;

namespace YoutubeTelegramBot.Controllers
{
    [Route("")]
    public class TelegramBotController : Controller
    {
        public readonly IUnitOfWork UnitOfWork;
        public readonly IBotService BotService;
        public readonly IYoutubeService YoutubeService;

        public TelegramBotController(IUnitOfWork unitOfWork, IBotService botService, IYoutubeService youtubeService)
        {
            UnitOfWork = unitOfWork;
            BotService = botService;
            YoutubeService = youtubeService;
        }

        [Route("/check")]
        public async Task<IActionResult> Index()
        {
            return Ok("okey man");
        }

        [HttpPost]
        public async Task<IActionResult> ExecuteCommand([FromBody]Update update)
        {
            var input = "";

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                input = update.Message.Text;
            }
            else if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                input = update.CallbackQuery.Data;
            }
            
            var messageText = ControllerHelper.ParseTelegramInput(input);

            var command = BotService.GetCommand(messageText[0]);

            if (command != null)
            {
                command.inputedData = messageText[1];

                await command.ExecuteAsync(update);
            }

            return Ok();
        }
    }
}

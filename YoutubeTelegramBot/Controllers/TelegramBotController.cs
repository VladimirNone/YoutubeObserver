using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using YoutubeTelegramBot.Infrastructure.Telegram.Interfaces;
using YoutubeTelegramBot.Infrastructure.Youtube.Interfaces;
using YoutubeTelegramBot.Repositories.Interfaces;

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
        public async Task<IActionResult> GetKuplinov([FromBody]Update update)
        {
            var command = BotService.GetCommand(update.Message.Text);
            if (command != null)
                await command.Execute(update.Message);

            return Ok();
        }
    }
}

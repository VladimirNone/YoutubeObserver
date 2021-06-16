using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoutubeTelegramBot.Repositories.Interfaces;

namespace YoutubeTelegramBot.Controllers
{
    public class TelegramBotController : Controller
    {
        public readonly IUnitOfWork UnitOfWork;

        public TelegramBotController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        [Route("/check")]
        public IActionResult Index()
        {
            return Ok("okey man");
        }

        [Route("/")]
        public async Task<IActionResult> GetKuplinov()
        {
            return Json(await UnitOfWork.ChannelsRepository.GetChannelWithVideosAsync(1));
        }
    }
}

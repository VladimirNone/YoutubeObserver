using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeTelegramBot.Repositories.Interfaces;
using YoutubeTelegramBot.Repositories.Implementations;
using YoutubeTelegramBot.Infrastructure.Telegram.Interfaces;
using YoutubeTelegramBot.Infrastructure.Telegram.Implementations;
using YoutubeTelegramBot.Infrastructure.Youtube.Interfaces;
using YoutubeTelegramBot.Infrastructure.Youtube.Implementations;

namespace YoutubeTelegramBot.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IYoutubeService, YoutubeService>();
            services.AddSingleton<IBotService, BotService>();
        }
    }
}

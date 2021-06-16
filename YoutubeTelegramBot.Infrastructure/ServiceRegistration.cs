using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeTelegramBot.Domain;
using YoutubeTelegramBot.Repositories;
using YoutubeTelegramBot.Repositories.Interfaces;
using YoutubeTelegramBot.Repositories.Repositories;

namespace YoutubeTelegramBot.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IChannelsRepository, ChannelsRepository>();
            services.AddTransient<IVideosRepository, VideosRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}

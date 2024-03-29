﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoutubeTelegramBot.Domain.POCOs;
using YoutubeTelegramBot.Infrastructure.Telegram.Implementations.Commands;
using YoutubeTelegramBot.Infrastructure.Telegram.Interfaces;
using YoutubeTelegramBot.Infrastructure.Youtube.Interfaces;
using YoutubeTelegramBot.Repositories.Interfaces;

namespace YoutubeTelegramBot.Infrastructure.BackgroundServices
{
    public class CheckVideosPeriodManagerService : BackgroundService
    {
        private IUnitOfWork UnitOfWork;
        private IBotService BotService;
        private readonly IYoutubeService YoutubeService;
        private readonly ILogger Logger;
        private readonly IServiceScopeFactory ScopeFactory;
        private readonly IConfiguration Configuration;

        public CheckVideosPeriodManagerService(IYoutubeService youtubeService, ILogger<CheckVideosPeriodManagerService> logger, IConfiguration configuration, IServiceScopeFactory scopeFactory)
        {
            YoutubeService = youtubeService;
            Logger = logger;
            Configuration = configuration;
            ScopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Logger.LogInformation("CheckVideosPeriodManagerService is starting");

            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = ScopeFactory.CreateScope();

                BotService = scope.ServiceProvider.GetRequiredService<IBotService>();
                UnitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                var channels = await UnitOfWork.ChannelsRepository.GetChannelsAsync();

                try
                {
                    foreach (var channel in channels)
                    {
                        var newVideos = await YoutubeService.SearchVideosAsync(channel);

                        for (int i = 0; i < newVideos.Count; i++)
                        {
                            if (await UnitOfWork.VideosRepository.VideoTracked(newVideos[i].youtube_id))
                            {
                                newVideos.Remove(newVideos[i]);
                            }
                        }

                        UnitOfWork.ChannelsRepository.MarkNewCheckChannel(channel, newVideos);
                    }

                    await UnitOfWork.Commit();

                    foreach (var channel in channels)
                    {
                        await BotService.ShowVideos(channel.Videos);
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex.Message);
                }

                await UnitOfWork.Rollback();

                var checkUpdateTime = Configuration.GetSection("CheckUpdateNewVideosTime");
                var period = new TimeSpan(int.Parse(checkUpdateTime["Hours"]), int.Parse(checkUpdateTime["Minutes"]), 0);

                Logger.LogInformation((DateTime.Now + period).ToString());

                await Task.Delay(period);
            }

            Logger.LogInformation("CheckVideosPeriodManagerService is stopping");
        }
    }
}

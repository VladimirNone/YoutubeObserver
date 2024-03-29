﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using YoutubeTelegramBot.Infrastructure.Telegram.Interfaces;
using YoutubeTelegramBot.Infrastructure.Youtube.Interfaces;
using YoutubeTelegramBot.Repositories.Interfaces;

namespace YoutubeTelegramBot.Infrastructure.Telegram.Implementations.Commands
{
    public class AddChannelCommand : Command
    {
        public override string Name => "/addChannel";

        public override string Description => "Добавляет канал по имени в список отслеживаемых";

        public AddChannelCommand(IBotService botService, IYoutubeService youtubeService, IUnitOfWork unitOfWork)
            : base(botService, youtubeService, unitOfWork)
        {
        }

        public override async Task ExecuteAsync(Update update)
        {
            var message = update.Message;

            if (string.IsNullOrEmpty(inputedData))
            {
                await botService.Client.SendTextMessageAsync(message.Chat.Id, $"Need to point out a name of channel");
                return;
            }

            var channel = (await youtubeService.SearchChannelsByNameAsync(inputedData))?.FirstOrDefault();

            if(channel == null)
            {
                await botService.Client.SendTextMessageAsync(message.Chat.Id, $"Channel with such name weren't founded");
                return;
            }

            if(await unitOfWork.ChannelsRepository.ChannelTracked(channel.youtube_id))
            {
                await botService.Client.SendTextMessageAsync(message.Chat.Id, $"Channel with name '{channel.name}' has already been added");
                return;
            }

            await unitOfWork.ChannelsRepository.AddEntityAsync(channel);
            await unitOfWork.Commit();
            await botService.Client.SendTextMessageAsync(message.Chat.Id, $"Channel with name '{channel.name}' was added");
        }
    }
}

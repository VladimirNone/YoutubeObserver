﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeTelegramBot.Domain.POCOs;

namespace YoutubeTelegramBot.Repositories.Interfaces
{
    public interface IChannelsRepository : IRepository<Channel>
    {
        Task<Channel> GetChannelWithVideosAsync(int channelId);
    }
}
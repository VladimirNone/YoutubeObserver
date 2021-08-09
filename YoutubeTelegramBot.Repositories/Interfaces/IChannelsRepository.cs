using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeTelegramBot.Domain.POCOs;

namespace YoutubeTelegramBot.Repositories.Interfaces
{
    public interface IChannelsRepository : IRepository<Channel>
    {
        Task<List<Channel>> GetChannelsAsync();
        Task<Channel> GetChannelByYoutubeIdAsync(string channelId);
        Task<Channel> GetChannelByNameAsync(string name);
        Task<bool> ChannelTracked(string channelId);
        void MarkNewCheckChannel(Channel channel, List<Video> newVideosToAdd);
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeTelegramBot.Domain.POCOs;
using YoutubeTelegramBot.Repositories.Interfaces;

namespace YoutubeTelegramBot.Repositories.Implementations
{
    public class ChannelsRepository : Repository<Channel>, IChannelsRepository
    {
        public ChannelsRepository(YoutubeObserverDbContext context)
            : base(context)
        {
        }

        public async Task<List<Channel>> GetChannelsAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<Channel> GetChannelByYoutubeIdAsync(string id)
        {
            return await DbSet.AsNoTracking().Include(h => h.Videos).FirstAsync(h => h.youtube_id == id);
        }

        public async Task<Channel> GetChannelByNameAsync(string name)
        {
            return await DbSet.FirstAsync(h => h.name == name);
        }

        /// <summary>
        /// Worked with the tracked channels
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="newVideosToAdd"></param>
        public void MarkNewCheckChannel(Channel channel, List<Video> newVideosToAdd)
        {
            if(channel.Videos == null)
            {
                channel.Videos = newVideosToAdd;
            }
            else
            {
                channel.Videos.AddRange(newVideosToAdd);
            }

            channel.last_check = DateTime.Now;
        }
    }
}

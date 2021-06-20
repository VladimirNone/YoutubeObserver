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

        public async Task<Channel> GetChannelAsync(string id)
        {
            return await DbSet.AsNoTracking().Include(h => h.Videos).FirstAsync(h => h.youtube_id == id);
        }
    }
}

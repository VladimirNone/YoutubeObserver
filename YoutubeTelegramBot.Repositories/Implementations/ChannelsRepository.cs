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

        public async Task<Channel> GetEntityAsync(string id)
        {
            return await DbSet.AsNoTracking().Include(h => h.Vidoes).FirstAsync(h => h.youtube_id == id);
        }
    }
}

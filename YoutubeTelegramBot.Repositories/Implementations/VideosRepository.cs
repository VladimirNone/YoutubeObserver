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
    public class VideosRepository : Repository<Video>, IVideosRepository
    {
        public VideosRepository(YoutubeObserverDbContext context)
            : base(context)
        {
        }

        public async Task<List<Video>> GetVideosAsync(VideoStatus status)
        {
            return await DbSet.AsNoTracking().Where(h => h.status == status).ToListAsync();
        }

        public async Task<bool> VideoTracked(string videoId)
        {
            return await DbSet.AsNoTracking().AnyAsync(h => h.youtube_id == videoId);
        }
    }
}

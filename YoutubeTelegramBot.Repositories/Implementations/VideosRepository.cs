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

    }
}

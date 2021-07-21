using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeTelegramBot.Domain.POCOs;

namespace YoutubeTelegramBot.Repositories.Interfaces
{
    public interface IVideosRepository : IRepository<Video>
    {
        Task<List<Video>> GetVideosAsync(VideoStatus status);
    }
}

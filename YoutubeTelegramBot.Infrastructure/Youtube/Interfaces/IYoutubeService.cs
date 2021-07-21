using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeTelegramBot.Domain.POCOs;

namespace YoutubeTelegramBot.Infrastructure.Youtube.Interfaces
{
    public interface IYoutubeService
    {
        public static string StartPartOfVideoUrl { get; } = "https://www.youtube.com/watch?v=";

        Task<List<Channel>> SearchChannelsByNameAsync(string name, bool exactly = true);
        Task<List<Video>> SearchVideosAsync(Channel channel);
        Task<Video> SearchVideoAsync(string videoYoutubeId);
    }
}

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
        List<Channel> SearchChannelsByName(string name, bool exactly = true);
        List<Video> SearchVideos(Channel channel, DateTime? publishedAfter);
    }
}

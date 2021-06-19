using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeTelegramBot.Domain.POCOs;
using YoutubeTelegramBot.Infrastructure.Youtube.Interfaces;

namespace YoutubeTelegramBot.Infrastructure.Youtube.Implementations
{
    public class YoutubeService : IYoutubeService
    {
        private YouTubeService youtubeService { get; set; }

        public YoutubeService(IConfiguration configuration)
        {
            youtubeService = new YouTubeService(new BaseClientService.Initializer
            {
                ApplicationName = "My Project 66809",
                ApiKey = configuration["YoutubeApiKey"],
            });


        }

        public List<Channel> SearchChannelsByName(string name, bool exactly)
        {
            var l = youtubeService.Search.List("snippet");
            l.Q = name;
            l.Type = "channel";
            var res = l.Execute();

            if (exactly)
            {
                var resultChannel = res.Items.FirstOrDefault(h => h.Snippet.ChannelTitle == name);
                return new List<Channel>() { new Channel() { name = resultChannel.Snippet.ChannelTitle, youtube_id = resultChannel.Snippet.ChannelId } };
            }
            else
            {
                var resultList = new List<Channel>();
                foreach (var item in res.Items)
                {
                    resultList.Add(new Channel() { name = item.Snippet.ChannelTitle, youtube_id = item.Snippet.ChannelId });
                }
                return resultList;
            }
        }

        public List<Video> SearchVideos(Channel channel, DateTime? publishedAfter)
        {
            var lVideos = youtubeService.Search.List("snippet");
            lVideos.ChannelId = channel.youtube_id;
            lVideos.Type = "video";

            if(publishedAfter != null)
                lVideos.PublishedAfter = publishedAfter;

            lVideos.Order = SearchResource.ListRequest.OrderEnum.Date;
            var res = lVideos.Execute();

            var lVideo = youtubeService.Videos.List("snippet");
            var resultVideos = new List<Video>();

            foreach (var item in res.Items)
            {
                lVideo.Id = item.Id.VideoId;
                var video = lVideo.Execute().Items[0];
                resultVideos.Add(new Video() { name = video.Snippet.Title, published = video.Snippet.PublishedAt.Value, url = "https://www.youtube.com/watch?v=" + video.Id, youtube_id = video.Id, channel_id = video.Snippet.ChannelId });
            }

            return resultVideos;
        }
    }
}

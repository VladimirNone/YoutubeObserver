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

        public async Task<List<Channel>> SearchChannelsByNameAsync(string name, bool exactly)
        {
            var l = youtubeService.Search.List("snippet");
            l.Q = name;
            l.Type = "channel";
            var res = await l.ExecuteAsync();

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

        public async Task<List<Video>> SearchVideosAsync(Channel channel)
        {
            var lVideos = youtubeService.Search.List("snippet");
            lVideos.ChannelId = channel.youtube_id;
            lVideos.Type = "video";

            if(channel.last_check != null)
                lVideos.PublishedAfter = channel.last_check;

            lVideos.Order = SearchResource.ListRequest.OrderEnum.Date;
            var channelVideos = await lVideos.ExecuteAsync();

            var lVideo = youtubeService.Videos.List("snippet");
            var resultVideos = new List<Video>();

            foreach (var item in channelVideos.Items)
            {
                lVideo.Id = item.Id.VideoId;
                var youtubeVideo = (await lVideo.ExecuteAsync()).Items[0];
                resultVideos.Add(new Video() { name = youtubeVideo.Snippet.Title, published = youtubeVideo.Snippet.PublishedAt.Value, url = IYoutubeService.StartPartOfVideoUrl + youtubeVideo.Id, youtube_id = youtubeVideo.Id, channel_id = youtubeVideo.Snippet.ChannelId });
            }

            return resultVideos;
        }

        public async Task<Video> SearchVideoAsync(string videoYoutubeId)
        {
            var lVideos = youtubeService.Videos.List("snippet");
            lVideos.Id = videoYoutubeId;
            lVideos.MaxResults = 1;

            var youtubeVideo = (await lVideos.ExecuteAsync()).Items[0];

            if(youtubeVideo != null)
                return new Video() { name = youtubeVideo.Snippet.Title, published = youtubeVideo.Snippet.PublishedAt.Value, url = IYoutubeService.StartPartOfVideoUrl + youtubeVideo.Id, youtube_id = youtubeVideo.Id, channel_id = youtubeVideo.Snippet.ChannelId };

            return null;
        }
    }
}

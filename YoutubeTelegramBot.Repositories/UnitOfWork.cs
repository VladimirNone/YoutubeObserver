using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeTelegramBot.Domain.POCOs;
using YoutubeTelegramBot.Repositories.Interfaces;

namespace YoutubeTelegramBot.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public YoutubeObserverDbContext Context { get; }
        public IVideosRepository VideosRepository { get; }
        public IChannelsRepository ChannelsRepository { get; }

        public UnitOfWork(YoutubeObserverDbContext context, IVideosRepository videosRepo, IChannelsRepository channelsRepo)
        {
            Context = context;

            VideosRepository = videosRepo;
            ChannelsRepository = channelsRepo;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }
    }
}

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
    public class UnitOfWork : IUnitOfWork
    {
        public Guid guidId { get; set; } = Guid.NewGuid();
        private IVideosRepository _videosRepository;
        private IChannelsRepository _channelsRepository;
        private YoutubeObserverDbContext context { get; }

        public IVideosRepository VideosRepository 
        {
            get => _videosRepository ??= new VideosRepository(context);
        }

        public IChannelsRepository ChannelsRepository
        {
            get => _channelsRepository ??= new ChannelsRepository(context);
        }

        public UnitOfWork(YoutubeObserverDbContext context)
        {
            this.context = context;
        }

        public async Task Commit()
        {
            await context.SaveChangesAsync();
        }

        public async Task Rollback()
        {
            await context.DisposeAsync();
        }
    }
}

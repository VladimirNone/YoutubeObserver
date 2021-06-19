using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeTelegramBot.Domain.POCOs;
using YoutubeTelegramBot.Repositories.Interfaces;

namespace YoutubeTelegramBot.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IVideosRepository VideosRepository { get; }
        IChannelsRepository ChannelsRepository { get; }

        Task Commit();
        Task Rollback();
    }
}

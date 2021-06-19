using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeTelegramBot.Domain;

namespace YoutubeTelegramBot.Repositories.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        DbSet<T> DbSet { get; }

        Task AddEntityAsync(T entity);
        void Update(T entity);
        Task<T> GetEntityAsync(int id);
    }
}

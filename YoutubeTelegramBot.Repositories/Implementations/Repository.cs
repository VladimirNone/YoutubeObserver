using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeTelegramBot.Domain;
using YoutubeTelegramBot.Domain.POCOs;
using YoutubeTelegramBot.Repositories.Interfaces;

namespace YoutubeTelegramBot.Repositories.Implementations
{
    public abstract class Repository<T> : IRepository<T> where T: Entity
    {
        public DbSet<T> DbSet { get; protected set; }

        public Repository(YoutubeObserverDbContext dbContext)
        {
            DbSet = dbContext.Set<T>();
        }

        public virtual async Task AddEntityAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            await DbSet.AddAsync(entity);
        }

        public virtual void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            DbSet.Update(entity);
        }

        public virtual async Task<T> GetEntityAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeTelegramBot.Domain;
using YoutubeTelegramBot.Domain.POCOs;
using YoutubeTelegramBot.Repositories.Interfaces;

namespace YoutubeTelegramBot.Repositories.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T: Entity
    {
        public DbSet<T> DbSet { get; protected set; }

        public Repository(YoutubeObserverDbContext dbContext)
        {
            DbSet = dbContext.Set<T>();
        }

        public void AddEntity(T entity)
        {
            DbSet.Add(entity);
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
        }

        public T GetEntity(int id)
        {
            return DbSet.Find(id);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeTelegramBot.Domain.POCOs;
using Npgsql;

namespace YoutubeTelegramBot.Repositories
{
    public class YoutubeObserverDbContext : DbContext
    {
        public YoutubeObserverDbContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Video> Videos { get; set; }
        public DbSet<Channel> Channels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Video>().HasKey(h => h.youtube_id);

            modelBuilder.Entity<Video>().ToTable("videos");

            modelBuilder.Entity<Channel>().HasKey(h => h.youtube_id);
            
            modelBuilder.Entity<Channel>().ToTable("channels");
        }
    }
}

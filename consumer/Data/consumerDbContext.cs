using consumer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consumer.Data
{
    public class consumerDbContext : DbContext
    {
        public consumerDbContext() { }
        public consumerDbContext(DbContextOptions options) : base(options) { }
        public DbSet<LiveStatus> liveStatus => Set<LiveStatus>();
        public DbSet<AssetLiveStatus> AssetLiveStatus => Set<AssetLiveStatus>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = "Server=localhost;Port=3306;Database=kafka-db;Uid=root;Pwd=root;";
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LiveStatus>()
                .HasKey(a => a.assetId);
            modelBuilder.Entity<AssetLiveStatus>()
                .HasKey(a => a.assetId);
        }

    }
}

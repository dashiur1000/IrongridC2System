using IronGridAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IronGridAPI.Data
{
    public class IronGridDbContext : DbContext
    {
        private readonly DbContextOptions _options;
        public IronGridDbContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }
        public DbSet<AssetLiveStatus> AssetLiveStatus => Set<AssetLiveStatus>();
        public DbSet<Units> Units => Set<Units>();

        public DbSet<Asset> Assets => Set<Asset>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Units>()
                .HasMany(a => a.Assets)
                .WithOne(a => a.Unit)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Asset>()
                .HasOne(a => a.Unit)
                .WithMany(a => a.Assets);
            modelBuilder.Entity<AssetLiveStatus>()
                .HasKey(a => a.assetId);

        }
    }
}

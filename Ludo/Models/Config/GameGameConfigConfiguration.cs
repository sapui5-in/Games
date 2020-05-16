using Microsoft.EntityFrameworkCore;

namespace Ludo.API.Model.Config
{
    public static class GameGameConfigConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameGameConfig>()
                .ToTable("GameGameConfigs")
                .HasKey(g => new { g.GameId, g.GameConfigId });

            modelBuilder.Entity<GameGameConfig>()
                .HasOne<Game>(a => a.Game)
                .WithMany(s => s.GameGameConfigs)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(a => a.GameId);

            modelBuilder.Entity<GameGameConfig>()
                .HasOne<GameConfig>(a => a.GameConfig)
                .WithMany(s => s.GameGameConfigs)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(a => a.GameConfigId);

        }
    }
}

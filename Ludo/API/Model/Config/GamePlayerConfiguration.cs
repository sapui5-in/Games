using Microsoft.EntityFrameworkCore;

namespace Ludo.API.Model.Config
{
    public static class GamePlayerConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GamePlayer>()
                .ToTable("GamesPlayers")
                .HasKey(g => new { g.GameId, g.PlayerId });

            modelBuilder.Entity<GamePlayer>()
                .HasOne<Game>(a => a.Game)
                .WithMany(s => s.GamePlayers)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(a => a.GameId);

            modelBuilder.Entity<GamePlayer>()
                .HasOne<User>(a => a.Player)
                .WithMany(s => s.GamePlayers)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(a => a.PlayerId);

        }
    }
}

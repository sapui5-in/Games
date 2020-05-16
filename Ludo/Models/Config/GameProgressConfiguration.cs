using Microsoft.EntityFrameworkCore;

namespace Ludo.API.Model.Config
{
    public static class GameProgressConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameProgress>()
                .ToTable("GamesProgresses")
                .HasKey(g => new { g.GameId});

            modelBuilder.Entity<GameProgress>()
                .HasOne<Game>(g => g.Game)
                .WithMany(gp => gp.GameProgresses)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(c => c.GameId);

            modelBuilder.Entity<GameProgress>()
                .HasOne<User>(g => g.CurrentPlayer)
                .WithMany(gp => gp.GameProgresses)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(c => c.CurrentPlayerId);
        }
    }
}

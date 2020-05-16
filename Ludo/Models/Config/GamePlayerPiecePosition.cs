using Microsoft.EntityFrameworkCore;

namespace Ludo.API.Model.Config
{
    public static class GamePlayerPiecePositionConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GamePlayerPiecePosition>()
                .ToTable("GamePlayerPiecePositions")
                .HasKey(g => new { g.GameId, g.PlayerId, g.PieceNumber });

            modelBuilder.Entity<GamePlayerPiecePosition>()
                .HasOne<Game>(a => a.Game)
                .WithMany(s => s.GamePlayerPiecePositions)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(a => a.GameId);

            modelBuilder.Entity<GamePlayerPiecePosition>()
                .HasOne<User>(a => a.Player)
                .WithMany(s => s.GamePlayerPiecePositions)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(a => a.PlayerId);
        }
    }
}

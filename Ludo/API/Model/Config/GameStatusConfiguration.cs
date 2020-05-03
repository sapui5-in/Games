using Microsoft.EntityFrameworkCore;

namespace Ludo.API.Model.Config
{
    public static class GameStatusConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameStatus>()
                .HasMany<Game>(g => g.Games)
                .WithOne(gs => gs.GameStatus)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(c => c.GameStatusId);
        }
    }
}
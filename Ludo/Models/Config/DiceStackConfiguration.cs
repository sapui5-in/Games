using Ludo.Models;
using Microsoft.EntityFrameworkCore;

namespace Ludo.API.Model.Config
{
    public static class DiceStackConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DiceStack>()
                .ToTable("DiceStacks")
                .HasKey(g => new { g.GameId });

            modelBuilder.Entity<DiceStack>()
                .HasOne<Game>(a => a.Game)
                .WithMany(s => s.DiceStacks)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(a => a.GameId);
        }
    }
}

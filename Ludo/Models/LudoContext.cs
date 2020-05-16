using Ludo.API.Model.Config;
using Ludo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ludo.API.Model
{
    public class LudoContext : DbContext
    {
        //public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    //.UseLoggerFactory(MyLoggerFactory)
                    .UseLazyLoadingProxies()
                    .UseSqlServer("Server=.;Database=Ludo;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            API.Model.Config.UserConfiguration.Configure(modelBuilder);

            API.Model.Config.AddressConfiguration.Configure(modelBuilder);

            API.Model.Config.GameProgressConfiguration.Configure(modelBuilder);

            API.Model.Config.GamePlayerConfiguration.Configure(modelBuilder);

            API.Model.Config.DiceStackConfiguration.Configure(modelBuilder);

            API.Model.Config.GamePlayerPiecePositionConfiguration.Configure(modelBuilder);

            API.Model.Config.GameStatusConfiguration.Configure(modelBuilder);

            API.Model.Config.GameGameConfigConfiguration.Configure(modelBuilder);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<GameProgress> GameProgresses { get; set; }

        public DbSet<GamePlayer> GamePlayers { get; set; }

        public DbSet<DiceStack> DiceStacks { get; set; }

        public DbSet<GamePlayerPiecePosition> GamePlayerPiecePositions { get; set; }

        public DbSet<GameStatus> GameStatuses { get; set; }
    }
}

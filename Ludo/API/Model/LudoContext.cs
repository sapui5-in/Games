using Ludo.API.Model.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            UserConfiguration.Configure(modelBuilder);

            AddressConfiguration.Configure(modelBuilder);

            GameProgressConfiguration.Configure(modelBuilder);

            GamePlayerConfiguration.Configure(modelBuilder);

            GamePlayerPiecePositionConfiguration.Configure(modelBuilder);

            GameStatusConfiguration.Configure(modelBuilder);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<GameProgress> GameProgresses { get; set; }

        public DbSet<GamePlayer> GamePlayers { get; set; }

        public DbSet<GamePlayerPiecePosition> GamePlayerPiecePositions { get; set; }

        public DbSet<GameStatus> GameStatuses { get; set; }
    }
}

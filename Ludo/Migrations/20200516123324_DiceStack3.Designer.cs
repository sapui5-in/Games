﻿// <auto-generated />
using System;
using Ludo.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ludo.Migrations
{
    [DbContext(typeof(LudoContext))]
    [Migration("20200516123324_DiceStack3")]
    partial class DiceStack3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Ludo.API.Model.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressLine1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostalCode")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Ludo.API.Model.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("GameStatusId")
                        .HasColumnType("int");

                    b.Property<string>("GameType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("GameStatusId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Ludo.API.Model.GameConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GameConfig");
                });

            modelBuilder.Entity("Ludo.API.Model.GameGameConfig", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("GameConfigId")
                        .HasColumnType("int");

                    b.Property<string>("GameConfigValue")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GameId", "GameConfigId");

                    b.HasIndex("GameConfigId");

                    b.ToTable("GameGameConfigs");
                });

            modelBuilder.Entity("Ludo.API.Model.GamePlayer", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("Quadrant")
                        .HasColumnType("int");

                    b.HasKey("GameId", "PlayerId");

                    b.HasIndex("PlayerId");

                    b.ToTable("GamesPlayers");
                });

            modelBuilder.Entity("Ludo.API.Model.GamePlayerPiecePosition", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("PieceNumber")
                        .HasColumnType("int");

                    b.Property<int>("GhorPosition")
                        .HasColumnType("int");

                    b.Property<string>("GhorType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quadrant")
                        .HasColumnType("int");

                    b.HasKey("GameId", "PlayerId", "PieceNumber");

                    b.HasIndex("PlayerId");

                    b.ToTable("GamePlayerPiecePositions");
                });

            modelBuilder.Entity("Ludo.API.Model.GameProgress", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<bool>("CurrentPlayerDiceRolled")
                        .HasColumnType("bit");

                    b.Property<int>("CurrentPlayerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastActionDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastDiceValue")
                        .HasColumnType("int");

                    b.HasKey("GameId");

                    b.HasIndex("CurrentPlayerId");

                    b.ToTable("GamesProgresses");
                });

            modelBuilder.Entity("Ludo.API.Model.GameStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GameStatuses");
                });

            modelBuilder.Entity("Ludo.API.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePic")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Ludo.Models.DiceStack", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("DiceValue1")
                        .HasColumnType("int");

                    b.Property<int>("DiceValue2")
                        .HasColumnType("int");

                    b.Property<int>("DiceValue3")
                        .HasColumnType("int");

                    b.HasKey("GameId");

                    b.ToTable("DiceStacks");
                });

            modelBuilder.Entity("Ludo.API.Model.Address", b =>
                {
                    b.HasOne("Ludo.API.Model.User", "User")
                        .WithOne("Address")
                        .HasForeignKey("Ludo.API.Model.Address", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Ludo.API.Model.Game", b =>
                {
                    b.HasOne("Ludo.API.Model.User", "User")
                        .WithMany("Games")
                        .HasForeignKey("CreatedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ludo.API.Model.GameStatus", "GameStatus")
                        .WithMany("Games")
                        .HasForeignKey("GameStatusId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Ludo.API.Model.GameGameConfig", b =>
                {
                    b.HasOne("Ludo.API.Model.GameConfig", "GameConfig")
                        .WithMany("GameGameConfigs")
                        .HasForeignKey("GameConfigId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Ludo.API.Model.Game", "Game")
                        .WithMany("GameGameConfigs")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Ludo.API.Model.GamePlayer", b =>
                {
                    b.HasOne("Ludo.API.Model.Game", "Game")
                        .WithMany("GamePlayers")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Ludo.API.Model.User", "Player")
                        .WithMany("GamePlayers")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Ludo.API.Model.GamePlayerPiecePosition", b =>
                {
                    b.HasOne("Ludo.API.Model.Game", "Game")
                        .WithMany("GamePlayerPiecePositions")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Ludo.API.Model.User", "Player")
                        .WithMany("GamePlayerPiecePositions")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Ludo.API.Model.GameProgress", b =>
                {
                    b.HasOne("Ludo.API.Model.User", "CurrentPlayer")
                        .WithMany("GameProgresses")
                        .HasForeignKey("CurrentPlayerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Ludo.API.Model.Game", "Game")
                        .WithMany("GameProgresses")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Ludo.Models.DiceStack", b =>
                {
                    b.HasOne("Ludo.API.Model.Game", "Game")
                        .WithMany("DiceStacks")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

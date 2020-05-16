using Microsoft.EntityFrameworkCore.Migrations;

namespace Ludo.Migrations
{
    public partial class GameConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameConfig",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameGameConfigs",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false),
                    GameConfigId = table.Column<int>(nullable: false),
                    GameConfigValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameGameConfigs", x => new { x.GameId, x.GameConfigId });
                    table.ForeignKey(
                        name: "FK_GameGameConfigs_GameConfig_GameConfigId",
                        column: x => x.GameConfigId,
                        principalTable: "GameConfig",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GameGameConfigs_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameGameConfigs_GameConfigId",
                table: "GameGameConfigs",
                column: "GameConfigId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameGameConfigs");

            migrationBuilder.DropTable(
                name: "GameConfig");
        }
    }
}

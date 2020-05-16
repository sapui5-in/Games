using Microsoft.EntityFrameworkCore.Migrations;

namespace Ludo.Migrations
{
    public partial class DiceStack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiceStacks",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    DiceValue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiceStacks", x => new { x.GameId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_DiceStacks_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DiceStacks_Users_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiceStacks_PlayerId",
                table: "DiceStacks",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiceStacks");
        }
    }
}

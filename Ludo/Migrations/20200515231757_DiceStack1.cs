using Microsoft.EntityFrameworkCore.Migrations;

namespace Ludo.Migrations
{
    public partial class DiceStack1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DiceStacks",
                table: "DiceStacks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiceStacks",
                table: "DiceStacks",
                columns: new[] { "GameId", "PlayerId", "DiceValue" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DiceStacks",
                table: "DiceStacks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiceStacks",
                table: "DiceStacks",
                columns: new[] { "GameId", "PlayerId" });
        }
    }
}

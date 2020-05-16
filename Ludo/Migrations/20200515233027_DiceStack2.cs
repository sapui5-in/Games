using Microsoft.EntityFrameworkCore.Migrations;

namespace Ludo.Migrations
{
    public partial class DiceStack2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiceStacks_Users_PlayerId",
                table: "DiceStacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiceStacks",
                table: "DiceStacks");

            migrationBuilder.DropIndex(
                name: "IX_DiceStacks_PlayerId",
                table: "DiceStacks");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "DiceStacks");

            migrationBuilder.DropColumn(
                name: "DiceValue",
                table: "DiceStacks");

            migrationBuilder.AddColumn<int>(
                name: "DiceValue1",
                table: "DiceStacks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiceValue2",
                table: "DiceStacks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiceStacks",
                table: "DiceStacks",
                column: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DiceStacks",
                table: "DiceStacks");

            migrationBuilder.DropColumn(
                name: "DiceValue1",
                table: "DiceStacks");

            migrationBuilder.DropColumn(
                name: "DiceValue2",
                table: "DiceStacks");

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "DiceStacks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiceValue",
                table: "DiceStacks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiceStacks",
                table: "DiceStacks",
                columns: new[] { "GameId", "PlayerId", "DiceValue" });

            migrationBuilder.CreateIndex(
                name: "IX_DiceStacks_PlayerId",
                table: "DiceStacks",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiceStacks_Users_PlayerId",
                table: "DiceStacks",
                column: "PlayerId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Ludo.Migrations
{
    public partial class DiceStack3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiceValue3",
                table: "DiceStacks",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiceValue3",
                table: "DiceStacks");
        }
    }
}

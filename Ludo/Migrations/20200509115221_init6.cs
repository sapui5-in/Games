using Microsoft.EntityFrameworkCore.Migrations;

namespace Ludo.Migrations
{
    public partial class init6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PieceGhorPosition",
                table: "GamePlayerPiecePositions");

            migrationBuilder.DropColumn(
                name: "PieceQuadrant",
                table: "GamePlayerPiecePositions");

            migrationBuilder.AddColumn<int>(
                name: "GhorPosition",
                table: "GamePlayerPiecePositions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GhorType",
                table: "GamePlayerPiecePositions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quadrant",
                table: "GamePlayerPiecePositions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GhorPosition",
                table: "GamePlayerPiecePositions");

            migrationBuilder.DropColumn(
                name: "GhorType",
                table: "GamePlayerPiecePositions");

            migrationBuilder.DropColumn(
                name: "Quadrant",
                table: "GamePlayerPiecePositions");

            migrationBuilder.AddColumn<int>(
                name: "PieceGhorPosition",
                table: "GamePlayerPiecePositions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PieceQuadrant",
                table: "GamePlayerPiecePositions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

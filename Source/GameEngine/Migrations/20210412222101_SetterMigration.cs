using Microsoft.EntityFrameworkCore.Migrations;

namespace GameEngine.Migrations
{
    public partial class SetterMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CharToDraw",
                table: "Pieces");

            migrationBuilder.AddColumn<int>(
                name: "Col",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Row",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Col",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Row",
                table: "Players");

            migrationBuilder.AddColumn<string>(
                name: "CharToDraw",
                table: "Pieces",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}

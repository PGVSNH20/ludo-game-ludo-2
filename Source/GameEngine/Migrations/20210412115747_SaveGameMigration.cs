using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameEngine.Migrations
{
    public partial class SaveGameMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameInProgress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    GamePiece1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GamePiece2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GamePiece3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GamePiece4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameInProgress", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameInProgress");
        }
    }
}

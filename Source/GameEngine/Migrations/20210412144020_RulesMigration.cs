using Microsoft.EntityFrameworkCore.Migrations;

namespace GameEngine.Migrations
{
    public partial class RulesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    RulesId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SaveGameId = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberOfPlayers = table.Column<int>(type: "INTEGER", nullable: false),
                    PiecesPerPlayer = table.Column<int>(type: "INTEGER", nullable: false),
                    ThrowAgainOnSixEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    InitialSixRuleEnabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.RulesId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rules");
        }
    }
}

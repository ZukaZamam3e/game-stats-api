using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStats.Data.Migrations
{
    /// <inheritdoc />
    public partial class _004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TOTAL_SCORE",
                table: "MATCH_TEAM");

            migrationBuilder.DropColumn(
                name: "TEAM_COLOR",
                table: "MATCH_PLAYER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TOTAL_SCORE",
                table: "MATCH_TEAM",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TEAM_COLOR",
                table: "MATCH_PLAYER",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

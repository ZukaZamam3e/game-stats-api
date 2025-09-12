using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStats.Data.Migrations
{
    /// <inheritdoc />
    public partial class _003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MATCH_TEAM_ID",
                table: "MATCH_PLAYER",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MATCH_ID",
                table: "MATCH_PLAYER",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MATCH_PLAYER_MATCH_ID",
                table: "MATCH_PLAYER",
                column: "MATCH_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_MATCH_PLAYER_MATCH_MATCH_ID",
                table: "MATCH_PLAYER",
                column: "MATCH_ID",
                principalTable: "MATCH",
                principalColumn: "MATCH_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MATCH_PLAYER_MATCH_MATCH_ID",
                table: "MATCH_PLAYER");

            migrationBuilder.DropIndex(
                name: "IX_MATCH_PLAYER_MATCH_ID",
                table: "MATCH_PLAYER");

            migrationBuilder.DropColumn(
                name: "MATCH_ID",
                table: "MATCH_PLAYER");

            migrationBuilder.AlterColumn<int>(
                name: "MATCH_TEAM_ID",
                table: "MATCH_PLAYER",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}

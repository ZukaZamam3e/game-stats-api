using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStats.Data.Migrations
{
    /// <inheritdoc />
    public partial class _002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MATCH_MAP_MAP_ID",
                table: "MATCH");

            migrationBuilder.DropForeignKey(
                name: "FK_MATCH_MATCH_TYPE_MATCH_TYPE_ID",
                table: "MATCH");

            migrationBuilder.AlterColumn<int>(
                name: "MATCH_TYPE_ID",
                table: "MATCH",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "OLD_MATCH_ID",
                table: "MATCH",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_MATCH_MAP_MAP_ID",
                table: "MATCH",
                column: "MAP_ID",
                principalTable: "MAP",
                principalColumn: "MAP_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MATCH_MATCH_TYPE_MATCH_TYPE_ID",
                table: "MATCH",
                column: "MATCH_TYPE_ID",
                principalTable: "MATCH_TYPE",
                principalColumn: "MATCH_TYPE_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MATCH_MAP_MAP_ID",
                table: "MATCH");

            migrationBuilder.DropForeignKey(
                name: "FK_MATCH_MATCH_TYPE_MATCH_TYPE_ID",
                table: "MATCH");

            migrationBuilder.DropColumn(
                name: "OLD_MATCH_ID",
                table: "MATCH");

            migrationBuilder.AlterColumn<int>(
                name: "MATCH_TYPE_ID",
                table: "MATCH",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MATCH_MAP_MAP_ID",
                table: "MATCH",
                column: "MAP_ID",
                principalTable: "MAP",
                principalColumn: "MAP_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MATCH_MATCH_TYPE_MATCH_TYPE_ID",
                table: "MATCH",
                column: "MATCH_TYPE_ID",
                principalTable: "MATCH_TYPE",
                principalColumn: "MATCH_TYPE_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

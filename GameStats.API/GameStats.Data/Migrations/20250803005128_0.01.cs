using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStats.Data.Migrations
{
    /// <inheritdoc />
    public partial class _001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CODE_VALUE",
                columns: table => new
                {
                    CODE_TABLE_ID = table.Column<int>(type: "int", nullable: false),
                    CODE_VALUE_ID = table.Column<int>(type: "int", nullable: false),
                    DECODE_TXT = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EXTRA_INFO = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CODE_VALUE", x => x.CODE_TABLE_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GAME",
                columns: table => new
                {
                    GAME_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GAME_NAME = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GAME", x => x.GAME_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MAP",
                columns: table => new
                {
                    MAP_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MAP_NAME = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GAME_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MAP", x => x.MAP_ID);
                    table.ForeignKey(
                        name: "FK_MAP_GAME_GAME_ID",
                        column: x => x.GAME_ID,
                        principalTable: "GAME",
                        principalColumn: "GAME_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MATCH_TYPE",
                columns: table => new
                {
                    MATCH_TYPE_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MATCH_TYPE_NAME = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GAME_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MATCH_TYPE", x => x.MATCH_TYPE_ID);
                    table.ForeignKey(
                        name: "FK_MATCH_TYPE_GAME_GAME_ID",
                        column: x => x.GAME_ID,
                        principalTable: "GAME",
                        principalColumn: "GAME_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PLAYER",
                columns: table => new
                {
                    PLAYER_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PLAYER_NAME = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GAME_ID = table.Column<int>(type: "int", nullable: false),
                    EMBLEM = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLAYER", x => x.PLAYER_ID);
                    table.ForeignKey(
                        name: "FK_PLAYER_GAME_GAME_ID",
                        column: x => x.GAME_ID,
                        principalTable: "GAME",
                        principalColumn: "GAME_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MATCH",
                columns: table => new
                {
                    MATCH_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GAME_ID = table.Column<int>(type: "int", nullable: false),
                    MATCH_NAME = table.Column<int>(type: "int", nullable: false),
                    TYPE_CD = table.Column<int>(type: "int", nullable: false),
                    MAP_ID = table.Column<int>(type: "int", nullable: false),
                    TOTAL_TIME = table.Column<int>(type: "int", nullable: true),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    MATCH_TYPE_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MATCH", x => x.MATCH_ID);
                    table.ForeignKey(
                        name: "FK_MATCH_GAME_GAME_ID",
                        column: x => x.GAME_ID,
                        principalTable: "GAME",
                        principalColumn: "GAME_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MATCH_MAP_MAP_ID",
                        column: x => x.MAP_ID,
                        principalTable: "MAP",
                        principalColumn: "MAP_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MATCH_MATCH_TYPE_MATCH_TYPE_ID",
                        column: x => x.MATCH_TYPE_ID,
                        principalTable: "MATCH_TYPE",
                        principalColumn: "MATCH_TYPE_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MATCH_TEAM",
                columns: table => new
                {
                    MATCH_TEAM_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MATCH_ID = table.Column<int>(type: "int", nullable: false),
                    TEAM_COLOR = table.Column<int>(type: "int", nullable: false),
                    TOTAL_SCORE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MATCH_TEAM", x => x.MATCH_TEAM_ID);
                    table.ForeignKey(
                        name: "FK_MATCH_TEAM_MATCH_MATCH_ID",
                        column: x => x.MATCH_ID,
                        principalTable: "MATCH",
                        principalColumn: "MATCH_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MATCH_PLAYER",
                columns: table => new
                {
                    MATCH_PLAYER_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MATCH_TEAM_ID = table.Column<int>(type: "int", nullable: false),
                    PLAYER_ID = table.Column<int>(type: "int", nullable: false),
                    TEAM_COLOR = table.Column<int>(type: "int", nullable: false),
                    SCORE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MATCH_PLAYER", x => x.MATCH_PLAYER_ID);
                    table.ForeignKey(
                        name: "FK_MATCH_PLAYER_MATCH_TEAM_MATCH_TEAM_ID",
                        column: x => x.MATCH_TEAM_ID,
                        principalTable: "MATCH_TEAM",
                        principalColumn: "MATCH_TEAM_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MATCH_PLAYER_PLAYER_PLAYER_ID",
                        column: x => x.PLAYER_ID,
                        principalTable: "PLAYER",
                        principalColumn: "PLAYER_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MAP_GAME_ID",
                table: "MAP",
                column: "GAME_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MATCH_GAME_ID",
                table: "MATCH",
                column: "GAME_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MATCH_MAP_ID",
                table: "MATCH",
                column: "MAP_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MATCH_MATCH_TYPE_ID",
                table: "MATCH",
                column: "MATCH_TYPE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MATCH_PLAYER_MATCH_TEAM_ID",
                table: "MATCH_PLAYER",
                column: "MATCH_TEAM_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MATCH_PLAYER_PLAYER_ID",
                table: "MATCH_PLAYER",
                column: "PLAYER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MATCH_TEAM_MATCH_ID",
                table: "MATCH_TEAM",
                column: "MATCH_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MATCH_TYPE_GAME_ID",
                table: "MATCH_TYPE",
                column: "GAME_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PLAYER_GAME_ID",
                table: "PLAYER",
                column: "GAME_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CODE_VALUE");

            migrationBuilder.DropTable(
                name: "MATCH_PLAYER");

            migrationBuilder.DropTable(
                name: "MATCH_TEAM");

            migrationBuilder.DropTable(
                name: "PLAYER");

            migrationBuilder.DropTable(
                name: "MATCH");

            migrationBuilder.DropTable(
                name: "MAP");

            migrationBuilder.DropTable(
                name: "MATCH_TYPE");

            migrationBuilder.DropTable(
                name: "GAME");
        }
    }
}

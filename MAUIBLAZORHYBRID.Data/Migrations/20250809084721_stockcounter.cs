using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class stockcounter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BarItemCounterStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CounterId = table.Column<int>(type: "INTEGER", nullable: false),
                    BarItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    Stock = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarItemCounterStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BarItemCounterStock_BarItems_BarItemId",
                        column: x => x.BarItemId,
                        principalTable: "BarItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BarItemCounterStock_BillStations_CounterId",
                        column: x => x.CounterId,
                        principalTable: "BillStations",
                        principalColumn: "billStationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SyncHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LastSyncTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SyncType = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyncHistories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BarItemCounterStock_BarItemId",
                table: "BarItemCounterStock",
                column: "BarItemId");

            migrationBuilder.CreateIndex(
                name: "IX_BarItemCounterStock_CounterId",
                table: "BarItemCounterStock",
                column: "CounterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BarItemCounterStock");

            migrationBuilder.DropTable(
                name: "SyncHistories");
        }
    }
}

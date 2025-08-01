using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class kottables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HotKOTMasters",
                columns: table => new
                {
                    AppKOTId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HotKOTId = table.Column<int>(type: "INTEGER", nullable: true),
                    HotKOTType = table.Column<int>(type: "INTEGER", nullable: false),
                    HotKOTPrefix = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    HotKOTRefNo = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    HotKOTDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HotKOTTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BearerID = table.Column<int>(type: "INTEGER", nullable: false),
                    NoOfGuests = table.Column<int>(type: "INTEGER", nullable: false),
                    HotKotAmt = table.Column<decimal>(type: "TEXT", precision: 20, scale: 3, nullable: false),
                    HotKOTNotes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    AppMachineID = table.Column<int>(type: "INTEGER", nullable: false),
                    BranchID = table.Column<int>(type: "INTEGER", nullable: false),
                    DiningSpaceID = table.Column<int>(type: "INTEGER", nullable: false),
                    EnteredEmpID = table.Column<int>(type: "INTEGER", nullable: false),
                    CounterID = table.Column<int>(type: "INTEGER", nullable: false),
                    IsSynced = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotKOTMasters", x => x.AppKOTId);
                });

            migrationBuilder.CreateTable(
                name: "HotKOTItemDetails",
                columns: table => new
                {
                    AppKOTItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemID = table.Column<int>(type: "INTEGER", nullable: false),
                    BarCode = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Qty = table.Column<decimal>(type: "TEXT", precision: 20, scale: 3, nullable: false),
                    UnitID = table.Column<int>(type: "INTEGER", nullable: false),
                    DetRate = table.Column<decimal>(type: "TEXT", precision: 20, scale: 3, nullable: false),
                    DetAmt = table.Column<decimal>(type: "TEXT", precision: 20, scale: 3, nullable: false),
                    DetDiscPer = table.Column<decimal>(type: "TEXT", precision: 20, scale: 3, nullable: false),
                    DetDiscAmt = table.Column<decimal>(type: "TEXT", precision: 20, scale: 3, nullable: false),
                    DetGrossAmt = table.Column<decimal>(type: "TEXT", precision: 20, scale: 3, nullable: false),
                    DetTaxPer = table.Column<decimal>(type: "TEXT", precision: 20, scale: 3, nullable: false),
                    DetTaxAmt = table.Column<decimal>(type: "TEXT", precision: 20, scale: 3, nullable: false),
                    DetNetAmt = table.Column<decimal>(type: "TEXT", precision: 20, scale: 3, nullable: false),
                    HotKotItemNotes = table.Column<string>(type: "TEXT", maxLength: 900, nullable: false),
                    AppKOTId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotKOTItemDetails", x => x.AppKOTItemId);
                    table.ForeignKey(
                        name: "FK_HotKOTItemDetails_HotKOTMasters_AppKOTId",
                        column: x => x.AppKOTId,
                        principalTable: "HotKOTMasters",
                        principalColumn: "AppKOTId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotKOTTables",
                columns: table => new
                {
                    AppKOTTableId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HotTabID = table.Column<int>(type: "INTEGER", nullable: false),
                    AppKOTId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotKOTTables", x => x.AppKOTTableId);
                    table.ForeignKey(
                        name: "FK_HotKOTTables_HotKOTMasters_AppKOTId",
                        column: x => x.AppKOTId,
                        principalTable: "HotKOTMasters",
                        principalColumn: "AppKOTId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotKOTItemDetails_AppKOTId",
                table: "HotKOTItemDetails",
                column: "AppKOTId");

            migrationBuilder.CreateIndex(
                name: "IX_HotKOTTables_AppKOTId",
                table: "HotKOTTables",
                column: "AppKOTId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotKOTItemDetails");

            migrationBuilder.DropTable(
                name: "HotKOTTables");

            migrationBuilder.DropTable(
                name: "HotKOTMasters");
        }
    }
}

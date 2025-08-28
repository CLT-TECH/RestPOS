using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class stockinward : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockInwardMasters",
                columns: table => new
                {
                    StockInwardId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SockInwardServerId = table.Column<int>(type: "INTEGER", nullable: false),
                    StockInwardSlNo = table.Column<int>(type: "INTEGER", nullable: false),
                    StockInwardPrefix = table.Column<string>(type: "TEXT", nullable: false),
                    StockInwardDocNo = table.Column<string>(type: "TEXT", nullable: false),
                    StockInwardRefNo = table.Column<string>(type: "TEXT", nullable: false),
                    StockInwardDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StockInwardTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StockInwardSqlDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    VendorId = table.Column<int>(type: "INTEGER", nullable: false),
                    StockInwardNotes = table.Column<string>(type: "TEXT", nullable: false),
                    BranchId = table.Column<int>(type: "INTEGER", nullable: false),
                    LoginEmpId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsSynced = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockInwardMasters", x => x.StockInwardId);
                    table.ForeignKey(
                        name: "FK_StockInwardMasters_BranchMasters_BranchId",
                        column: x => x.BranchId,
                        principalTable: "BranchMasters",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockInwardMasters_VendorMasters_VendorId",
                        column: x => x.VendorId,
                        principalTable: "VendorMasters",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockInwardDetails",
                columns: table => new
                {
                    StockInwardDetId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StockInwardId = table.Column<int>(type: "INTEGER", nullable: false),
                    BarItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    InwardQty = table.Column<decimal>(type: "TEXT", nullable: false),
                    UnitId = table.Column<int>(type: "INTEGER", nullable: false),
                    DetConversion = table.Column<decimal>(type: "TEXT", nullable: false),
                    InwardBaseQty = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockInwardDetails", x => x.StockInwardDetId);
                    table.ForeignKey(
                        name: "FK_StockInwardDetails_BillItems_BarItemId",
                        column: x => x.BarItemId,
                        principalTable: "BillItems",
                        principalColumn: "itemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockInwardDetails_StockInwardMasters_StockInwardId",
                        column: x => x.StockInwardId,
                        principalTable: "StockInwardMasters",
                        principalColumn: "StockInwardId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockInwardDetails_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "unitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockInwardDetails_BarItemId",
                table: "StockInwardDetails",
                column: "BarItemId");

            migrationBuilder.CreateIndex(
                name: "IX_StockInwardDetails_StockInwardId",
                table: "StockInwardDetails",
                column: "StockInwardId");

            migrationBuilder.CreateIndex(
                name: "IX_StockInwardDetails_UnitId",
                table: "StockInwardDetails",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_StockInwardMasters_BranchId",
                table: "StockInwardMasters",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_StockInwardMasters_VendorId",
                table: "StockInwardMasters",
                column: "VendorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockInwardDetails");

            migrationBuilder.DropTable(
                name: "StockInwardMasters");
        }
    }
}

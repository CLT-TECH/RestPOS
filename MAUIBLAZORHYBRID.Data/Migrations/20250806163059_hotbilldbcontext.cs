using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class hotbilldbcontext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HotBillMasters",
                columns: table => new
                {
                    HotBillId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HotBillType = table.Column<int>(type: "INTEGER", nullable: false),
                    HotBillPrefix = table.Column<string>(type: "TEXT", nullable: false),
                    HotBillNo = table.Column<int>(type: "INTEGER", nullable: false),
                    HotBillRefNo = table.Column<string>(type: "TEXT", nullable: false),
                    HotBillDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HotBillTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BearerId = table.Column<int>(type: "INTEGER", nullable: false),
                    HotBillItemTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    HotBillTaxTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    B4roundAmt = table.Column<decimal>(type: "TEXT", nullable: false),
                    RoundNeed = table.Column<int>(type: "INTEGER", nullable: false),
                    RoundOffVal = table.Column<decimal>(type: "TEXT", nullable: false),
                    HotBillNeTAmt = table.Column<decimal>(type: "TEXT", nullable: false),
                    CashierFound = table.Column<int>(type: "INTEGER", nullable: false),
                    HotBillNotes = table.Column<string>(type: "TEXT", nullable: false),
                    AppMachineId = table.Column<int>(type: "INTEGER", nullable: false),
                    BranchId = table.Column<int>(type: "INTEGER", nullable: false),
                    DiningSpaceId = table.Column<int>(type: "INTEGER", nullable: false),
                    EnteredEmpId = table.Column<int>(type: "INTEGER", nullable: false),
                    CounterId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerMobile = table.Column<string>(type: "TEXT", nullable: false),
                    IsSynced = table.Column<bool>(type: "INTEGER", nullable: false),
                    ServerHotBillId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotBillMasters", x => x.HotBillId);
                });

            migrationBuilder.CreateTable(
                name: "HotBillAgainstKots",
                columns: table => new
                {
                    HotBillKotId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HotBillId = table.Column<int>(type: "INTEGER", nullable: false),
                    HotKotId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotBillAgainstKots", x => x.HotBillKotId);
                    table.ForeignKey(
                        name: "FK_HotBillAgainstKots_HotBillMasters_HotBillId",
                        column: x => x.HotBillId,
                        principalTable: "HotBillMasters",
                        principalColumn: "HotBillId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotBillAgainstKots_HotKOTMasters_HotKotId",
                        column: x => x.HotKotId,
                        principalTable: "HotKOTMasters",
                        principalColumn: "HotKOTId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotBillItemDetail",
                columns: table => new
                {
                    HotBillItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HotBillId = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    BarCode = table.Column<string>(type: "TEXT", nullable: false),
                    Qty = table.Column<decimal>(type: "TEXT", nullable: false),
                    UnitId = table.Column<int>(type: "INTEGER", nullable: false),
                    DetAmt = table.Column<decimal>(type: "TEXT", nullable: false),
                    DetDiscPer = table.Column<decimal>(type: "TEXT", nullable: false),
                    DetDiscAmt = table.Column<decimal>(type: "TEXT", nullable: false),
                    DetGrossAmt = table.Column<decimal>(type: "TEXT", nullable: false),
                    DetTaxPer = table.Column<decimal>(type: "TEXT", nullable: false),
                    DetTaxAmt = table.Column<decimal>(type: "TEXT", nullable: false),
                    DetNetAmt = table.Column<decimal>(type: "TEXT", nullable: false),
                    DetRate = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotBillItemDetail", x => x.HotBillItemId);
                    table.ForeignKey(
                        name: "FK_HotBillItemDetail_HotBillMasters_HotBillId",
                        column: x => x.HotBillId,
                        principalTable: "HotBillMasters",
                        principalColumn: "HotBillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotBillTaxDetails",
                columns: table => new
                {
                    HotBillTaxId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HotBillId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaXId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaxableAmt = table.Column<decimal>(type: "TEXT", nullable: false),
                    TaxPer = table.Column<decimal>(type: "TEXT", nullable: false),
                    TaxAmt = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotBillTaxDetails", x => x.HotBillTaxId);
                    table.ForeignKey(
                        name: "FK_HotBillTaxDetails_HotBillMasters_HotBillId",
                        column: x => x.HotBillId,
                        principalTable: "HotBillMasters",
                        principalColumn: "HotBillId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotBillTaxDetails_TaxMasters_TaXId",
                        column: x => x.TaXId,
                        principalTable: "TaxMasters",
                        principalColumn: "TaxId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotBillAgainstKots_HotBillId",
                table: "HotBillAgainstKots",
                column: "HotBillId");

            migrationBuilder.CreateIndex(
                name: "IX_HotBillAgainstKots_HotKotId",
                table: "HotBillAgainstKots",
                column: "HotKotId");

            migrationBuilder.CreateIndex(
                name: "IX_HotBillItemDetail_HotBillId",
                table: "HotBillItemDetail",
                column: "HotBillId");

            migrationBuilder.CreateIndex(
                name: "IX_HotBillTaxDetails_HotBillId",
                table: "HotBillTaxDetails",
                column: "HotBillId");

            migrationBuilder.CreateIndex(
                name: "IX_HotBillTaxDetails_TaXId",
                table: "HotBillTaxDetails",
                column: "TaXId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotBillAgainstKots");

            migrationBuilder.DropTable(
                name: "HotBillItemDetail");

            migrationBuilder.DropTable(
                name: "HotBillTaxDetails");

            migrationBuilder.DropTable(
                name: "HotBillMasters");
        }
    }
}

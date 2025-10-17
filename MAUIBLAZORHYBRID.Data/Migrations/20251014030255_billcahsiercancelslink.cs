using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class billcahsiercancelslink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillCashierCancels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BillCashierId = table.Column<int>(type: "INTEGER", nullable: false),
                    CancelReason = table.Column<string>(type: "TEXT", nullable: false),
                    CancelDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CancelTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CancelledByEmpId = table.Column<int>(type: "INTEGER", nullable: false),
                    BranchId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsSynced = table.Column<bool>(type: "INTEGER", nullable: false),
                    BillCashierCancelServerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillCashierCancels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillCashierCancels_BillCashiers_BillCashierId",
                        column: x => x.BillCashierId,
                        principalTable: "BillCashiers",
                        principalColumn: "BillCashierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotBillCancels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HotBillId = table.Column<int>(type: "INTEGER", nullable: false),
                    CancelReason = table.Column<string>(type: "TEXT", nullable: false),
                    CancelDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CancelTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CancelledByEmpId = table.Column<int>(type: "INTEGER", nullable: false),
                    BranchId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsSynced = table.Column<bool>(type: "INTEGER", nullable: false),
                    HotBillCancelServerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotBillCancels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotBillCancels_HotBillMasters_HotBillId",
                        column: x => x.HotBillId,
                        principalTable: "HotBillMasters",
                        principalColumn: "HotBillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillCashierCancels_BillCashierId",
                table: "BillCashierCancels",
                column: "BillCashierId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HotBillCancels_HotBillId",
                table: "HotBillCancels",
                column: "HotBillId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillCashierCancels");

            migrationBuilder.DropTable(
                name: "HotBillCancels");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class billcashier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillCashier",
                columns: table => new
                {
                    BillCashierId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FormType = table.Column<int>(type: "INTEGER", nullable: false),
                    PaymentMode = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    TenderCash = table.Column<decimal>(type: "TEXT", nullable: false),
                    HotBillId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillCashier", x => x.BillCashierId);
                    table.ForeignKey(
                        name: "FK_BillCashier_HotBillMasters_HotBillId",
                        column: x => x.HotBillId,
                        principalTable: "HotBillMasters",
                        principalColumn: "HotBillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillCashier_HotBillId",
                table: "BillCashier",
                column: "HotBillId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillCashier");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class billcashierdbcontextadd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillCashier_HotBillMasters_HotBillId",
                table: "BillCashier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillCashier",
                table: "BillCashier");

            migrationBuilder.RenameTable(
                name: "BillCashier",
                newName: "BillCashiers");

            migrationBuilder.RenameIndex(
                name: "IX_BillCashier_HotBillId",
                table: "BillCashiers",
                newName: "IX_BillCashiers_HotBillId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillCashiers",
                table: "BillCashiers",
                column: "BillCashierId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillCashiers_HotBillMasters_HotBillId",
                table: "BillCashiers",
                column: "HotBillId",
                principalTable: "HotBillMasters",
                principalColumn: "HotBillId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillCashiers_HotBillMasters_HotBillId",
                table: "BillCashiers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillCashiers",
                table: "BillCashiers");

            migrationBuilder.RenameTable(
                name: "BillCashiers",
                newName: "BillCashier");

            migrationBuilder.RenameIndex(
                name: "IX_BillCashiers_HotBillId",
                table: "BillCashier",
                newName: "IX_BillCashier_HotBillId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillCashier",
                table: "BillCashier",
                column: "BillCashierId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillCashier_HotBillMasters_HotBillId",
                table: "BillCashier",
                column: "HotBillId",
                principalTable: "HotBillMasters",
                principalColumn: "HotBillId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

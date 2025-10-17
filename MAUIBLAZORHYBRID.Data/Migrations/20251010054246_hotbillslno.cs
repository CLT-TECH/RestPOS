using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class hotbillslno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HotBillCashNo",
                table: "BillCashiers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "HotBillCashPrefix",
                table: "BillCashiers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HotBillCashRefNo",
                table: "BillCashiers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HotBillCashNo",
                table: "BillCashiers");

            migrationBuilder.DropColumn(
                name: "HotBillCashPrefix",
                table: "BillCashiers");

            migrationBuilder.DropColumn(
                name: "HotBillCashRefNo",
                table: "BillCashiers");
        }
    }
}

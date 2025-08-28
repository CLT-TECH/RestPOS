using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class stockinwarddetailsdelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DetConversion",
                table: "StockInwardDetails");

            migrationBuilder.DropColumn(
                name: "InwardBaseQty",
                table: "StockInwardDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DetConversion",
                table: "StockInwardDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "InwardBaseQty",
                table: "StockInwardDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }
    }
}

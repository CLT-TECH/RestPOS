using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class stockcounters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarItemCounterStock_BarItems_BarItemId",
                table: "BarItemCounterStock");

            migrationBuilder.DropForeignKey(
                name: "FK_BarItemCounterStock_BillStations_CounterId",
                table: "BarItemCounterStock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BarItemCounterStock",
                table: "BarItemCounterStock");

            migrationBuilder.RenameTable(
                name: "BarItemCounterStock",
                newName: "BarItemCounterStocks");

            migrationBuilder.RenameIndex(
                name: "IX_BarItemCounterStock_CounterId",
                table: "BarItemCounterStocks",
                newName: "IX_BarItemCounterStocks_CounterId");

            migrationBuilder.RenameIndex(
                name: "IX_BarItemCounterStock_BarItemId",
                table: "BarItemCounterStocks",
                newName: "IX_BarItemCounterStocks_BarItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BarItemCounterStocks",
                table: "BarItemCounterStocks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BarItemCounterStocks_BarItems_BarItemId",
                table: "BarItemCounterStocks",
                column: "BarItemId",
                principalTable: "BarItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BarItemCounterStocks_BillStations_CounterId",
                table: "BarItemCounterStocks",
                column: "CounterId",
                principalTable: "BillStations",
                principalColumn: "billStationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarItemCounterStocks_BarItems_BarItemId",
                table: "BarItemCounterStocks");

            migrationBuilder.DropForeignKey(
                name: "FK_BarItemCounterStocks_BillStations_CounterId",
                table: "BarItemCounterStocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BarItemCounterStocks",
                table: "BarItemCounterStocks");

            migrationBuilder.RenameTable(
                name: "BarItemCounterStocks",
                newName: "BarItemCounterStock");

            migrationBuilder.RenameIndex(
                name: "IX_BarItemCounterStocks_CounterId",
                table: "BarItemCounterStock",
                newName: "IX_BarItemCounterStock_CounterId");

            migrationBuilder.RenameIndex(
                name: "IX_BarItemCounterStocks_BarItemId",
                table: "BarItemCounterStock",
                newName: "IX_BarItemCounterStock_BarItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BarItemCounterStock",
                table: "BarItemCounterStock",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BarItemCounterStock_BarItems_BarItemId",
                table: "BarItemCounterStock",
                column: "BarItemId",
                principalTable: "BarItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BarItemCounterStock_BillStations_CounterId",
                table: "BarItemCounterStock",
                column: "CounterId",
                principalTable: "BillStations",
                principalColumn: "billStationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

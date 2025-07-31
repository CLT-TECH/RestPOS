using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class rateitemrefitemtabletobillitem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiningSpaceItemRates_Items_itemId",
                table: "DiningSpaceItemRates");

            migrationBuilder.AddForeignKey(
                name: "FK_DiningSpaceItemRates_BillItems_itemId",
                table: "DiningSpaceItemRates",
                column: "itemId",
                principalTable: "BillItems",
                principalColumn: "itemId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiningSpaceItemRates_BillItems_itemId",
                table: "DiningSpaceItemRates");

            migrationBuilder.AddForeignKey(
                name: "FK_DiningSpaceItemRates_Items_itemId",
                table: "DiningSpaceItemRates",
                column: "itemId",
                principalTable: "Items",
                principalColumn: "itemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

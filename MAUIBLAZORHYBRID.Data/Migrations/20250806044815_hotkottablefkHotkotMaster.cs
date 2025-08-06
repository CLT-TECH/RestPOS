using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class hotkottablefkHotkotMaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HOTKotBilleds_HotKOTMasters_AppKOTId",
                table: "HOTKotBilleds");

            migrationBuilder.DropForeignKey(
                name: "FK_HotKOTItemDetails_HotKOTMasters_AppKOTId",
                table: "HotKOTItemDetails");

            migrationBuilder.RenameColumn(
                name: "AppKOTId",
                table: "HotKOTItemDetails",
                newName: "HotKOTId");

            migrationBuilder.RenameIndex(
                name: "IX_HotKOTItemDetails_AppKOTId",
                table: "HotKOTItemDetails",
                newName: "IX_HotKOTItemDetails_HotKOTId");

            migrationBuilder.RenameColumn(
                name: "AppKOTId",
                table: "HOTKotBilleds",
                newName: "HotKOTId");

            migrationBuilder.RenameIndex(
                name: "IX_HOTKotBilleds_AppKOTId",
                table: "HOTKotBilleds",
                newName: "IX_HOTKotBilleds_HotKOTId");

            migrationBuilder.AddForeignKey(
                name: "FK_HOTKotBilleds_HotKOTMasters_HotKOTId",
                table: "HOTKotBilleds",
                column: "HotKOTId",
                principalTable: "HotKOTMasters",
                principalColumn: "HotKOTId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotKOTItemDetails_HotKOTMasters_HotKOTId",
                table: "HotKOTItemDetails",
                column: "HotKOTId",
                principalTable: "HotKOTMasters",
                principalColumn: "HotKOTId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HOTKotBilleds_HotKOTMasters_HotKOTId",
                table: "HOTKotBilleds");

            migrationBuilder.DropForeignKey(
                name: "FK_HotKOTItemDetails_HotKOTMasters_HotKOTId",
                table: "HotKOTItemDetails");

            migrationBuilder.RenameColumn(
                name: "HotKOTId",
                table: "HotKOTItemDetails",
                newName: "AppKOTId");

            migrationBuilder.RenameIndex(
                name: "IX_HotKOTItemDetails_HotKOTId",
                table: "HotKOTItemDetails",
                newName: "IX_HotKOTItemDetails_AppKOTId");

            migrationBuilder.RenameColumn(
                name: "HotKOTId",
                table: "HOTKotBilleds",
                newName: "AppKOTId");

            migrationBuilder.RenameIndex(
                name: "IX_HOTKotBilleds_HotKOTId",
                table: "HOTKotBilleds",
                newName: "IX_HOTKotBilleds_AppKOTId");

            migrationBuilder.AddForeignKey(
                name: "FK_HOTKotBilleds_HotKOTMasters_AppKOTId",
                table: "HOTKotBilleds",
                column: "AppKOTId",
                principalTable: "HotKOTMasters",
                principalColumn: "HotKOTId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotKOTItemDetails_HotKOTMasters_AppKOTId",
                table: "HotKOTItemDetails",
                column: "AppKOTId",
                principalTable: "HotKOTMasters",
                principalColumn: "HotKOTId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class taxmasters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchTaxSettings_TaxMaster_TaxId",
                table: "BranchTaxSettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaxMaster",
                table: "TaxMaster");

            migrationBuilder.RenameTable(
                name: "TaxMaster",
                newName: "TaxMasters");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaxMasters",
                table: "TaxMasters",
                column: "TaxId");

            migrationBuilder.AddForeignKey(
                name: "FK_BranchTaxSettings_TaxMasters_TaxId",
                table: "BranchTaxSettings",
                column: "TaxId",
                principalTable: "TaxMasters",
                principalColumn: "TaxId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchTaxSettings_TaxMasters_TaxId",
                table: "BranchTaxSettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaxMasters",
                table: "TaxMasters");

            migrationBuilder.RenameTable(
                name: "TaxMasters",
                newName: "TaxMaster");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaxMaster",
                table: "TaxMaster",
                column: "TaxId");

            migrationBuilder.AddForeignKey(
                name: "FK_BranchTaxSettings_TaxMaster_TaxId",
                table: "BranchTaxSettings",
                column: "TaxId",
                principalTable: "TaxMaster",
                principalColumn: "TaxId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

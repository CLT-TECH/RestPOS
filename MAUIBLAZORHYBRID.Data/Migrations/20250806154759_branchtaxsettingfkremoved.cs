using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class branchtaxsettingfkremoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchTaxSettings_TaxMasters_TaxId",
                table: "BranchTaxSettings");

            migrationBuilder.DropIndex(
                name: "IX_BranchTaxSettings_TaxId",
                table: "BranchTaxSettings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BranchTaxSettings_TaxId",
                table: "BranchTaxSettings",
                column: "TaxId");

            migrationBuilder.AddForeignKey(
                name: "FK_BranchTaxSettings_TaxMasters_TaxId",
                table: "BranchTaxSettings",
                column: "TaxId",
                principalTable: "TaxMasters",
                principalColumn: "TaxId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

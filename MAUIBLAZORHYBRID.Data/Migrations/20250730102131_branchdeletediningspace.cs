using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class branchdeletediningspace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiningSpaces_BranchMasters_BranchMasterbranchId",
                table: "DiningSpaces");

            migrationBuilder.DropIndex(
                name: "IX_DiningSpaces_BranchMasterbranchId",
                table: "DiningSpaces");

            migrationBuilder.DropColumn(
                name: "BranchMasterbranchId",
                table: "DiningSpaces");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchMasterbranchId",
                table: "DiningSpaces",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiningSpaces_BranchMasterbranchId",
                table: "DiningSpaces",
                column: "BranchMasterbranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiningSpaces_BranchMasters_BranchMasterbranchId",
                table: "DiningSpaces",
                column: "BranchMasterbranchId",
                principalTable: "BranchMasters",
                principalColumn: "branchId");
        }
    }
}

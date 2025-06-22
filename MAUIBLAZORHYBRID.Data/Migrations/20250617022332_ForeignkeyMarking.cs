using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class ForeignkeyMarking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_SubCategories_SubCategorysubCatId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_Categories_CategorycatId",
                table: "SubCategories");

            migrationBuilder.DropIndex(
                name: "IX_SubCategories_CategorycatId",
                table: "SubCategories");

            migrationBuilder.DropIndex(
                name: "IX_Items_SubCategorysubCatId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CategorycatId",
                table: "SubCategories");

            migrationBuilder.DropColumn(
                name: "SubCategorysubCatId",
                table: "Items");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_catId",
                table: "SubCategories",
                column: "catId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_subCatId",
                table: "Items",
                column: "subCatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_SubCategories_subCatId",
                table: "Items",
                column: "subCatId",
                principalTable: "SubCategories",
                principalColumn: "subCatId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_Categories_catId",
                table: "SubCategories",
                column: "catId",
                principalTable: "Categories",
                principalColumn: "catId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_SubCategories_subCatId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_Categories_catId",
                table: "SubCategories");

            migrationBuilder.DropIndex(
                name: "IX_SubCategories_catId",
                table: "SubCategories");

            migrationBuilder.DropIndex(
                name: "IX_Items_subCatId",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "CategorycatId",
                table: "SubCategories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubCategorysubCatId",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategorycatId",
                table: "SubCategories",
                column: "CategorycatId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_SubCategorysubCatId",
                table: "Items",
                column: "SubCategorysubCatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_SubCategories_SubCategorysubCatId",
                table: "Items",
                column: "SubCategorysubCatId",
                principalTable: "SubCategories",
                principalColumn: "subCatId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_Categories_CategorycatId",
                table: "SubCategories",
                column: "CategorycatId",
                principalTable: "Categories",
                principalColumn: "catId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

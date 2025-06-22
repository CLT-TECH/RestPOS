using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class itemsSubcat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_SubCategories_SubCategorysubCatId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "catId",
                table: "Items",
                newName: "subCatId");

            migrationBuilder.AlterColumn<int>(
                name: "SubCategorysubCatId",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_SubCategories_SubCategorysubCatId",
                table: "Items",
                column: "SubCategorysubCatId",
                principalTable: "SubCategories",
                principalColumn: "subCatId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_SubCategories_SubCategorysubCatId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "subCatId",
                table: "Items",
                newName: "catId");

            migrationBuilder.AlterColumn<int>(
                name: "SubCategorysubCatId",
                table: "Items",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_SubCategories_SubCategorysubCatId",
                table: "Items",
                column: "SubCategorysubCatId",
                principalTable: "SubCategories",
                principalColumn: "subCatId");
        }
    }
}

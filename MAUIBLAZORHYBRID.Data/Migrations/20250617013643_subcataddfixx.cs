using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class subcataddfixx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubCategorysubCatId",
                table: "Items",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    subCatId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    subCatName = table.Column<string>(type: "TEXT", nullable: false),
                    catId = table.Column<int>(type: "INTEGER", nullable: false),
                    CategorycatId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.subCatId);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_CategorycatId",
                        column: x => x.CategorycatId,
                        principalTable: "Categories",
                        principalColumn: "catId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_SubCategorysubCatId",
                table: "Items",
                column: "SubCategorysubCatId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategorycatId",
                table: "SubCategories",
                column: "CategorycatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_SubCategories_SubCategorysubCatId",
                table: "Items",
                column: "SubCategorysubCatId",
                principalTable: "SubCategories",
                principalColumn: "subCatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_SubCategories_SubCategorysubCatId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropIndex(
                name: "IX_Items_SubCategorysubCatId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SubCategorysubCatId",
                table: "Items");
        }
    }
}

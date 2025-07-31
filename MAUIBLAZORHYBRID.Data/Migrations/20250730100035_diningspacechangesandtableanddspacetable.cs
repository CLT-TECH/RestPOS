using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class diningspacechangesandtableanddspacetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiningSpaces_BranchMasters_branchId",
                table: "DiningSpaces");

            migrationBuilder.DropIndex(
                name: "IX_DiningSpaces_branchId",
                table: "DiningSpaces");

            migrationBuilder.DropColumn(
                name: "branchId",
                table: "DiningSpaces");

            migrationBuilder.AlterColumn<int>(
                name: "unitId",
                table: "Units",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "subCatId",
                table: "SubCategories",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "diningSpaceId",
                table: "DiningSpaces",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "BranchMasterbranchId",
                table: "DiningSpaces",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "catId",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "unitId",
                table: "Units",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "subCatId",
                table: "SubCategories",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "diningSpaceId",
                table: "DiningSpaces",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "branchId",
                table: "DiningSpaces",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "catId",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateIndex(
                name: "IX_DiningSpaces_branchId",
                table: "DiningSpaces",
                column: "branchId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiningSpaces_BranchMasters_branchId",
                table: "DiningSpaces",
                column: "branchId",
                principalTable: "BranchMasters",
                principalColumn: "branchId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

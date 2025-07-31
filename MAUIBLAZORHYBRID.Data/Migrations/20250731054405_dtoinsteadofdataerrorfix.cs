using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class dtoinsteadofdataerrorfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tableid",
                table: "DiningSpaceTabless",
                newName: "tableId");

            migrationBuilder.RenameColumn(
                name: "diningspaceid",
                table: "DiningSpaceTabless",
                newName: "diningspaceId");

            migrationBuilder.RenameColumn(
                name: "branchid",
                table: "DiningSpaceTabless",
                newName: "branchId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "DiningSpaceTabless",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "DiningSpaceTabless",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tableId",
                table: "DiningSpaceTabless",
                newName: "tableid");

            migrationBuilder.RenameColumn(
                name: "diningspaceId",
                table: "DiningSpaceTabless",
                newName: "diningspaceid");

            migrationBuilder.RenameColumn(
                name: "branchId",
                table: "DiningSpaceTabless",
                newName: "branchid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DiningSpaceTabless",
                newName: "id");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "DiningSpaceTabless",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);
        }
    }
}

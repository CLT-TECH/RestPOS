using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class tablenamefix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DiningSpaceTabless",
                table: "DiningSpaceTabless");

            migrationBuilder.RenameTable(
                name: "DiningSpaceTabless",
                newName: "TablesDiningSpaces");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TablesDiningSpaces",
                table: "TablesDiningSpaces",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TablesDiningSpaces",
                table: "TablesDiningSpaces");

            migrationBuilder.RenameTable(
                name: "TablesDiningSpaces",
                newName: "DiningSpaceTabless");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiningSpaceTabless",
                table: "DiningSpaceTabless",
                column: "Id");
        }
    }
}

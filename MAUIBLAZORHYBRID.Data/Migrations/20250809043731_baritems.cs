using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class baritems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BarItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BarItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    BarItemCode = table.Column<string>(type: "TEXT", nullable: false),
                    BarItemName = table.Column<string>(type: "TEXT", nullable: false),
                    BarItemBaseUnitId = table.Column<int>(type: "INTEGER", nullable: false),
                    BarItemInventoryUnitId = table.Column<int>(type: "INTEGER", nullable: false),
                    MainBarItem = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarItems", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BarItems");
        }
    }
}

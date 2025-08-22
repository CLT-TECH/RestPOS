using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class addgodownstock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BarItemGodownStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GodownId = table.Column<int>(type: "INTEGER", nullable: false),
                    BarItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    Stock = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarItemGodownStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BarItemGodownStocks_BarItems_BarItemId",
                        column: x => x.BarItemId,
                        principalTable: "BarItems",
                        principalColumn: "BarItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BarItemGodownStocks_BarItemId",
                table: "BarItemGodownStocks",
                column: "BarItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BarItemGodownStocks");
        }
    }
}

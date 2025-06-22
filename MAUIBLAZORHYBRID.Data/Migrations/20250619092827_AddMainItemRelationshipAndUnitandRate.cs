using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMainItemRelationshipAndUnitandRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MainItemId",
                table: "Items",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "itemRate",
                table: "Items",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "unitId",
                table: "Items",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MainItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    unitId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    unitName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.unitId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_MainItemId",
                table: "Items",
                column: "MainItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_unitId",
                table: "Items",
                column: "unitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_MainItem_MainItemId",
                table: "Items",
                column: "MainItemId",
                principalTable: "MainItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Unit_unitId",
                table: "Items",
                column: "unitId",
                principalTable: "Unit",
                principalColumn: "unitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_MainItem_MainItemId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Unit_unitId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "MainItem");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Items_MainItemId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_unitId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "MainItemId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "itemRate",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "unitId",
                table: "Items");
        }
    }
}

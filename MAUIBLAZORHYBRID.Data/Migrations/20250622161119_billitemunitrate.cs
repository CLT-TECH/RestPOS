using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class billitemunitrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillItems",
                columns: table => new
                {
                    itemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    itemName = table.Column<string>(type: "TEXT", nullable: false),
                    itemType = table.Column<int>(type: "INTEGER", nullable: false),
                    CatId = table.Column<int>(type: "INTEGER", nullable: false),
                    categorycatId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillItems", x => x.itemId);
                    table.ForeignKey(
                        name: "FK_BillItems_Categories_categorycatId",
                        column: x => x.categorycatId,
                        principalTable: "Categories",
                        principalColumn: "catId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiningSpaceItemRates",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    itemId = table.Column<int>(type: "INTEGER", nullable: false),
                    diningSpaceId = table.Column<int>(type: "INTEGER", nullable: false),
                    itemRate = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiningSpaceItemRates", x => x.id);
                    table.ForeignKey(
                        name: "FK_DiningSpaceItemRates_DiningSpaces_diningSpaceId",
                        column: x => x.diningSpaceId,
                        principalTable: "DiningSpaces",
                        principalColumn: "diningSpaceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiningSpaceItemRates_Items_itemId",
                        column: x => x.itemId,
                        principalTable: "Items",
                        principalColumn: "itemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillItemUnits",
                columns: table => new
                {
                    itemUnitId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    itemId = table.Column<int>(type: "INTEGER", nullable: false),
                    unitId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillItemUnits", x => x.itemUnitId);
                    table.ForeignKey(
                        name: "FK_BillItemUnits_BillItems_itemId",
                        column: x => x.itemId,
                        principalTable: "BillItems",
                        principalColumn: "itemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillItemUnits_Units_unitId",
                        column: x => x.unitId,
                        principalTable: "Units",
                        principalColumn: "unitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_categorycatId",
                table: "BillItems",
                column: "categorycatId");

            migrationBuilder.CreateIndex(
                name: "IX_BillItemUnits_itemId",
                table: "BillItemUnits",
                column: "itemId");

            migrationBuilder.CreateIndex(
                name: "IX_BillItemUnits_unitId",
                table: "BillItemUnits",
                column: "unitId");

            migrationBuilder.CreateIndex(
                name: "IX_DiningSpaceItemRates_diningSpaceId",
                table: "DiningSpaceItemRates",
                column: "diningSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_DiningSpaceItemRates_itemId",
                table: "DiningSpaceItemRates",
                column: "itemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillItemUnits");

            migrationBuilder.DropTable(
                name: "DiningSpaceItemRates");

            migrationBuilder.DropTable(
                name: "BillItems");
        }
    }
}

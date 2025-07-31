using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class itemparentchild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemParentChilds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    childItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    parentItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    unitId = table.Column<int>(type: "INTEGER", nullable: false),
                    parentItemcode = table.Column<string>(type: "TEXT", nullable: false),
                    parentItemname = table.Column<string>(type: "TEXT", nullable: false),
                    childItemcode = table.Column<string>(type: "TEXT", nullable: false),
                    childItemname = table.Column<string>(type: "TEXT", nullable: false),
                    itemtype = table.Column<int>(type: "INTEGER", nullable: false),
                    CatId = table.Column<int>(type: "INTEGER", nullable: false),
                    categorycatId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemParentChilds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemParentChilds_Categories_categorycatId",
                        column: x => x.categorycatId,
                        principalTable: "Categories",
                        principalColumn: "catId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemParentChilds_Units_unitId",
                        column: x => x.unitId,
                        principalTable: "Units",
                        principalColumn: "unitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemParentChilds_categorycatId",
                table: "ItemParentChilds",
                column: "categorycatId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemParentChilds_unitId",
                table: "ItemParentChilds",
                column: "unitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemParentChilds");
        }
    }
}

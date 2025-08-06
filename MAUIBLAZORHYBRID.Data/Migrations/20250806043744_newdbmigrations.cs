using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class newdbmigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BranchMasters",
                columns: table => new
                {
                    branchId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    branchName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchMasters", x => x.branchId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    catId = table.Column<int>(type: "INTEGER", nullable: false),
                    catName = table.Column<string>(type: "TEXT", nullable: true),
                    catType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.catId);
                });

            migrationBuilder.CreateTable(
                name: "DiningSpaces",
                columns: table => new
                {
                    diningSpaceId = table.Column<int>(type: "INTEGER", nullable: false),
                    diningSpaceName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiningSpaces", x => x.diningSpaceId);
                });

            migrationBuilder.CreateTable(
                name: "HotKOTMasters",
                columns: table => new
                {
                    HotKOTId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HotKOTType = table.Column<int>(type: "INTEGER", nullable: false),
                    HotKOTNo = table.Column<int>(type: "INTEGER", nullable: false),
                    HotKOTPrefix = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    HotKOTRefNo = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    HotKOTDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HotKOTTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BearerID = table.Column<int>(type: "INTEGER", nullable: false),
                    NoOfGuests = table.Column<int>(type: "INTEGER", nullable: false),
                    HotKotAmt = table.Column<decimal>(type: "TEXT", precision: 20, scale: 3, nullable: false),
                    HotKOTNotes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    AppMachineID = table.Column<int>(type: "INTEGER", nullable: false),
                    BranchID = table.Column<int>(type: "INTEGER", nullable: false),
                    DiningSpaceID = table.Column<int>(type: "INTEGER", nullable: false),
                    EnteredEmpID = table.Column<int>(type: "INTEGER", nullable: false),
                    CounterID = table.Column<int>(type: "INTEGER", nullable: false),
                    IsSynced = table.Column<bool>(type: "INTEGER", nullable: false),
                    ServerKOTId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotKOTMasters", x => x.HotKOTId);
                });

            migrationBuilder.CreateTable(
                name: "MainItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    MainItemType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    tableId = table.Column<int>(type: "INTEGER", nullable: false),
                    tableName = table.Column<string>(type: "TEXT", nullable: false),
                    noOfSeats = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.tableId);
                });

            migrationBuilder.CreateTable(
                name: "TablesDiningSpaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    tableId = table.Column<int>(type: "INTEGER", nullable: false),
                    diningspaceId = table.Column<int>(type: "INTEGER", nullable: false),
                    branchId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TablesDiningSpaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    unitId = table.Column<int>(type: "INTEGER", nullable: false),
                    unitName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.unitId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BillStations",
                columns: table => new
                {
                    billStationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    billStationName = table.Column<string>(type: "TEXT", nullable: false),
                    branchId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillStations", x => x.billStationId);
                    table.ForeignKey(
                        name: "FK_BillStations_BranchMasters_branchId",
                        column: x => x.branchId,
                        principalTable: "BranchMasters",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "SubCategories",
                columns: table => new
                {
                    subCatId = table.Column<int>(type: "INTEGER", nullable: false),
                    subCatName = table.Column<string>(type: "TEXT", nullable: false),
                    catId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.subCatId);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_catId",
                        column: x => x.catId,
                        principalTable: "Categories",
                        principalColumn: "catId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HOTKotBilleds",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HOTKotBillID = table.Column<int>(type: "INTEGER", nullable: false),
                    AppKOTId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HOTKotBilleds", x => x.id);
                    table.ForeignKey(
                        name: "FK_HOTKotBilleds_HotKOTMasters_AppKOTId",
                        column: x => x.AppKOTId,
                        principalTable: "HotKOTMasters",
                        principalColumn: "HotKOTId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotKOTItemDetails",
                columns: table => new
                {
                    AppKOTItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemID = table.Column<int>(type: "INTEGER", nullable: false),
                    BarCode = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Qty = table.Column<decimal>(type: "TEXT", precision: 20, scale: 3, nullable: false),
                    UnitID = table.Column<int>(type: "INTEGER", nullable: false),
                    DetRate = table.Column<decimal>(type: "TEXT", precision: 20, scale: 3, nullable: false),
                    DetAmt = table.Column<decimal>(type: "TEXT", precision: 20, scale: 3, nullable: false),
                    DetDiscPer = table.Column<decimal>(type: "TEXT", precision: 20, scale: 3, nullable: false),
                    DetDiscAmt = table.Column<decimal>(type: "TEXT", precision: 20, scale: 3, nullable: false),
                    DetGrossAmt = table.Column<decimal>(type: "TEXT", precision: 20, scale: 3, nullable: false),
                    DetTaxPer = table.Column<decimal>(type: "TEXT", precision: 20, scale: 3, nullable: false),
                    DetTaxAmt = table.Column<decimal>(type: "TEXT", precision: 20, scale: 3, nullable: false),
                    DetNetAmt = table.Column<decimal>(type: "TEXT", precision: 20, scale: 3, nullable: false),
                    HotKotItemNotes = table.Column<string>(type: "TEXT", maxLength: 900, nullable: false),
                    AppKOTId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotKOTItemDetails", x => x.AppKOTItemId);
                    table.ForeignKey(
                        name: "FK_HotKOTItemDetails_HotKOTMasters_AppKOTId",
                        column: x => x.AppKOTId,
                        principalTable: "HotKOTMasters",
                        principalColumn: "HotKOTId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HOTKotCheckTabless",
                columns: table => new
                {
                    AppKOTTableId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HotTabID = table.Column<int>(type: "INTEGER", nullable: false),
                    AppKOTId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HOTKotCheckTabless", x => x.AppKOTTableId);
                    table.ForeignKey(
                        name: "FK_HOTKotCheckTabless_HotKOTMasters_AppKOTId",
                        column: x => x.AppKOTId,
                        principalTable: "HotKOTMasters",
                        principalColumn: "HotKOTId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HOTKotCheckTabless_Tables_HotTabID",
                        column: x => x.HotTabID,
                        principalTable: "Tables",
                        principalColumn: "tableId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotKOTTables",
                columns: table => new
                {
                    AppKOTTableId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HotTabID = table.Column<int>(type: "INTEGER", nullable: false),
                    HotKOTId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotKOTTables", x => x.AppKOTTableId);
                    table.ForeignKey(
                        name: "FK_HotKOTTables_HotKOTMasters_HotKOTId",
                        column: x => x.HotKOTId,
                        principalTable: "HotKOTMasters",
                        principalColumn: "HotKOTId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotKOTTables_Tables_HotTabID",
                        column: x => x.HotTabID,
                        principalTable: "Tables",
                        principalColumn: "tableId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                        name: "FK_DiningSpaceItemRates_BillItems_itemId",
                        column: x => x.itemId,
                        principalTable: "BillItems",
                        principalColumn: "itemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiningSpaceItemRates_DiningSpaces_diningSpaceId",
                        column: x => x.diningSpaceId,
                        principalTable: "DiningSpaces",
                        principalColumn: "diningSpaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    itemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    itemName = table.Column<string>(type: "TEXT", nullable: false),
                    subCatId = table.Column<int>(type: "INTEGER", nullable: false),
                    picURL = table.Column<string>(type: "TEXT", nullable: true),
                    itemRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    MainItemId = table.Column<int>(type: "INTEGER", nullable: true),
                    unitId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.itemId);
                    table.ForeignKey(
                        name: "FK_Items_MainItems_MainItemId",
                        column: x => x.MainItemId,
                        principalTable: "MainItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_SubCategories_subCatId",
                        column: x => x.subCatId,
                        principalTable: "SubCategories",
                        principalColumn: "subCatId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_Units_unitId",
                        column: x => x.unitId,
                        principalTable: "Units",
                        principalColumn: "unitId");
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
                name: "IX_BillStations_branchId",
                table: "BillStations",
                column: "branchId");

            migrationBuilder.CreateIndex(
                name: "IX_DiningSpaceItemRates_diningSpaceId",
                table: "DiningSpaceItemRates",
                column: "diningSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_DiningSpaceItemRates_itemId",
                table: "DiningSpaceItemRates",
                column: "itemId");

            migrationBuilder.CreateIndex(
                name: "IX_HOTKotBilleds_AppKOTId",
                table: "HOTKotBilleds",
                column: "AppKOTId");

            migrationBuilder.CreateIndex(
                name: "IX_HOTKotCheckTabless_AppKOTId",
                table: "HOTKotCheckTabless",
                column: "AppKOTId");

            migrationBuilder.CreateIndex(
                name: "IX_HOTKotCheckTabless_HotTabID",
                table: "HOTKotCheckTabless",
                column: "HotTabID");

            migrationBuilder.CreateIndex(
                name: "IX_HotKOTItemDetails_AppKOTId",
                table: "HotKOTItemDetails",
                column: "AppKOTId");

            migrationBuilder.CreateIndex(
                name: "IX_HotKOTTables_HotKOTId",
                table: "HotKOTTables",
                column: "HotKOTId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HotKOTTables_HotTabID",
                table: "HotKOTTables",
                column: "HotTabID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemParentChilds_categorycatId",
                table: "ItemParentChilds",
                column: "categorycatId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemParentChilds_unitId",
                table: "ItemParentChilds",
                column: "unitId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_MainItemId",
                table: "Items",
                column: "MainItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_subCatId",
                table: "Items",
                column: "subCatId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_unitId",
                table: "Items",
                column: "unitId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_catId",
                table: "SubCategories",
                column: "catId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillItemUnits");

            migrationBuilder.DropTable(
                name: "BillStations");

            migrationBuilder.DropTable(
                name: "DiningSpaceItemRates");

            migrationBuilder.DropTable(
                name: "HOTKotBilleds");

            migrationBuilder.DropTable(
                name: "HOTKotCheckTabless");

            migrationBuilder.DropTable(
                name: "HotKOTItemDetails");

            migrationBuilder.DropTable(
                name: "HotKOTTables");

            migrationBuilder.DropTable(
                name: "ItemParentChilds");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "TablesDiningSpaces");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "BranchMasters");

            migrationBuilder.DropTable(
                name: "BillItems");

            migrationBuilder.DropTable(
                name: "DiningSpaces");

            migrationBuilder.DropTable(
                name: "HotKOTMasters");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "MainItems");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}

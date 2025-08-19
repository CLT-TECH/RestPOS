using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BarItems",
                columns: table => new
                {
                    BarItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    BarItemCode = table.Column<string>(type: "TEXT", nullable: false),
                    BarItemName = table.Column<string>(type: "TEXT", nullable: false),
                    BarItemBaseUnitId = table.Column<int>(type: "INTEGER", nullable: false),
                    BarItemInventoryUnitId = table.Column<int>(type: "INTEGER", nullable: false),
                    MainBarItem = table.Column<int>(type: "INTEGER", nullable: false),
                    MainBarItemID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarItems", x => x.BarItemId);
                });

            migrationBuilder.CreateTable(
                name: "BranchMasters",
                columns: table => new
                {
                    branchId = table.Column<int>(type: "INTEGER", nullable: false),
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
                name: "HotBillMasters",
                columns: table => new
                {
                    HotBillId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HotBillType = table.Column<int>(type: "INTEGER", nullable: false),
                    HotBillPrefix = table.Column<string>(type: "TEXT", nullable: false),
                    HotBillNo = table.Column<int>(type: "INTEGER", nullable: false),
                    HotBillRefNo = table.Column<string>(type: "TEXT", nullable: false),
                    HotBillDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HotBillTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BearerId = table.Column<int>(type: "INTEGER", nullable: false),
                    HotBillItemTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    HotBillTaxTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    B4roundAmt = table.Column<decimal>(type: "TEXT", nullable: false),
                    RoundNeed = table.Column<int>(type: "INTEGER", nullable: false),
                    RoundOffVal = table.Column<decimal>(type: "TEXT", nullable: false),
                    HotBillNeTAmt = table.Column<decimal>(type: "TEXT", nullable: false),
                    CashierFound = table.Column<int>(type: "INTEGER", nullable: false),
                    HotBillNotes = table.Column<string>(type: "TEXT", nullable: false),
                    AppMachineId = table.Column<int>(type: "INTEGER", nullable: false),
                    BranchId = table.Column<int>(type: "INTEGER", nullable: false),
                    DiningSpaceId = table.Column<int>(type: "INTEGER", nullable: false),
                    EnteredEmpId = table.Column<int>(type: "INTEGER", nullable: false),
                    CounterId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerMobile = table.Column<string>(type: "TEXT", nullable: false),
                    IsSynced = table.Column<bool>(type: "INTEGER", nullable: false),
                    ServerHotBillId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotBillMasters", x => x.HotBillId);
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
                name: "StockTransfers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ServerTransferId = table.Column<int>(type: "INTEGER", nullable: false),
                    Prefix = table.Column<string>(type: "TEXT", nullable: false),
                    StkTrSlNo = table.Column<int>(type: "INTEGER", nullable: false),
                    RefNo = table.Column<string>(type: "TEXT", nullable: false),
                    TransferDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TransferTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FromType = table.Column<int>(type: "INTEGER", nullable: false),
                    ToType = table.Column<int>(type: "INTEGER", nullable: false),
                    FromGodownId = table.Column<int>(type: "INTEGER", nullable: false),
                    FromCounterId = table.Column<int>(type: "INTEGER", nullable: false),
                    ToGodownId = table.Column<int>(type: "INTEGER", nullable: false),
                    ToCounterId = table.Column<int>(type: "INTEGER", nullable: false),
                    BranchId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: false),
                    IsSynced = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransfers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SyncHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LastSyncTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SyncType = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyncHistories", x => x.Id);
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
                name: "TaxMasters",
                columns: table => new
                {
                    TaxId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaxCode = table.Column<string>(type: "TEXT", nullable: true),
                    TaxName = table.Column<string>(type: "TEXT", nullable: true),
                    TaxGroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaxCalcId = table.Column<int>(type: "INTEGER", nullable: false),
                    ApplicableType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxMasters", x => x.TaxId);
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
                    billStationId = table.Column<int>(type: "INTEGER", nullable: false),
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
                name: "BranchTaxSettings",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    BranchId = table.Column<int>(type: "INTEGER", nullable: false),
                    BillingType = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemType = table.Column<int>(type: "INTEGER", nullable: false),
                    TaxId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaxPer = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchTaxSettings", x => x.id);
                    table.ForeignKey(
                        name: "FK_BranchTaxSettings_BranchMasters_BranchId",
                        column: x => x.BranchId,
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
                name: "HotBillItemDetail",
                columns: table => new
                {
                    HotBillItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HotBillId = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    BarCode = table.Column<string>(type: "TEXT", nullable: false),
                    Qty = table.Column<decimal>(type: "TEXT", nullable: false),
                    UnitId = table.Column<int>(type: "INTEGER", nullable: false),
                    DetAmt = table.Column<decimal>(type: "TEXT", nullable: false),
                    DetDiscPer = table.Column<decimal>(type: "TEXT", nullable: false),
                    DetDiscAmt = table.Column<decimal>(type: "TEXT", nullable: false),
                    DetGrossAmt = table.Column<decimal>(type: "TEXT", nullable: false),
                    DetTaxPer = table.Column<decimal>(type: "TEXT", nullable: false),
                    DetTaxAmt = table.Column<decimal>(type: "TEXT", nullable: false),
                    DetNetAmt = table.Column<decimal>(type: "TEXT", nullable: false),
                    DetRate = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotBillItemDetail", x => x.HotBillItemId);
                    table.ForeignKey(
                        name: "FK_HotBillItemDetail_HotBillMasters_HotBillId",
                        column: x => x.HotBillId,
                        principalTable: "HotBillMasters",
                        principalColumn: "HotBillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotBillAgainstKots",
                columns: table => new
                {
                    HotBillKotId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HotBillId = table.Column<int>(type: "INTEGER", nullable: false),
                    HotKotId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotBillAgainstKots", x => x.HotBillKotId);
                    table.ForeignKey(
                        name: "FK_HotBillAgainstKots_HotBillMasters_HotBillId",
                        column: x => x.HotBillId,
                        principalTable: "HotBillMasters",
                        principalColumn: "HotBillId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotBillAgainstKots_HotKOTMasters_HotKotId",
                        column: x => x.HotKotId,
                        principalTable: "HotKOTMasters",
                        principalColumn: "HotKOTId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HOTKotBilleds",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HOTKotBillID = table.Column<int>(type: "INTEGER", nullable: false),
                    HotKOTId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HOTKotBilleds", x => x.id);
                    table.ForeignKey(
                        name: "FK_HOTKotBilleds_HotKOTMasters_HotKOTId",
                        column: x => x.HotKOTId,
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
                    HotKOTId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotKOTItemDetails", x => x.AppKOTItemId);
                    table.ForeignKey(
                        name: "FK_HotKOTItemDetails_HotKOTMasters_HotKOTId",
                        column: x => x.HotKOTId,
                        principalTable: "HotKOTMasters",
                        principalColumn: "HotKOTId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockTransferItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StockTransferLocalId = table.Column<int>(type: "INTEGER", nullable: false),
                    MainBarItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<decimal>(type: "TEXT", nullable: false),
                    ItemName = table.Column<string>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: false),
                    StkTrId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransferItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockTransferItems_StockTransfers_StkTrId",
                        column: x => x.StkTrId,
                        principalTable: "StockTransfers",
                        principalColumn: "Id",
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
                name: "HotBillTaxDetails",
                columns: table => new
                {
                    HotBillTaxId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HotBillId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaXId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaxableAmt = table.Column<decimal>(type: "TEXT", nullable: false),
                    TaxPer = table.Column<decimal>(type: "TEXT", nullable: false),
                    TaxAmt = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotBillTaxDetails", x => x.HotBillTaxId);
                    table.ForeignKey(
                        name: "FK_HotBillTaxDetails_HotBillMasters_HotBillId",
                        column: x => x.HotBillId,
                        principalTable: "HotBillMasters",
                        principalColumn: "HotBillId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotBillTaxDetails_TaxMasters_TaXId",
                        column: x => x.TaXId,
                        principalTable: "TaxMasters",
                        principalColumn: "TaxId",
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
                name: "BarItemCounterStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CounterId = table.Column<int>(type: "INTEGER", nullable: false),
                    BarItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    Stock = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarItemCounterStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BarItemCounterStocks_BarItems_BarItemId",
                        column: x => x.BarItemId,
                        principalTable: "BarItems",
                        principalColumn: "BarItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BarItemCounterStocks_BillStations_CounterId",
                        column: x => x.CounterId,
                        principalTable: "BillStations",
                        principalColumn: "billStationId",
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
                    itemId = table.Column<int>(type: "INTEGER", nullable: false),
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
                name: "IX_BarItemCounterStocks_BarItemId",
                table: "BarItemCounterStocks",
                column: "BarItemId");

            migrationBuilder.CreateIndex(
                name: "IX_BarItemCounterStocks_CounterId",
                table: "BarItemCounterStocks",
                column: "CounterId");

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
                name: "IX_BranchTaxSettings_BranchId",
                table: "BranchTaxSettings",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_DiningSpaceItemRates_diningSpaceId",
                table: "DiningSpaceItemRates",
                column: "diningSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_DiningSpaceItemRates_itemId",
                table: "DiningSpaceItemRates",
                column: "itemId");

            migrationBuilder.CreateIndex(
                name: "IX_HotBillAgainstKots_HotBillId",
                table: "HotBillAgainstKots",
                column: "HotBillId");

            migrationBuilder.CreateIndex(
                name: "IX_HotBillAgainstKots_HotKotId",
                table: "HotBillAgainstKots",
                column: "HotKotId");

            migrationBuilder.CreateIndex(
                name: "IX_HotBillItemDetail_HotBillId",
                table: "HotBillItemDetail",
                column: "HotBillId");

            migrationBuilder.CreateIndex(
                name: "IX_HotBillTaxDetails_HotBillId",
                table: "HotBillTaxDetails",
                column: "HotBillId");

            migrationBuilder.CreateIndex(
                name: "IX_HotBillTaxDetails_TaXId",
                table: "HotBillTaxDetails",
                column: "TaXId");

            migrationBuilder.CreateIndex(
                name: "IX_HOTKotBilleds_HotKOTId",
                table: "HOTKotBilleds",
                column: "HotKOTId");

            migrationBuilder.CreateIndex(
                name: "IX_HOTKotCheckTabless_AppKOTId",
                table: "HOTKotCheckTabless",
                column: "AppKOTId");

            migrationBuilder.CreateIndex(
                name: "IX_HOTKotCheckTabless_HotTabID",
                table: "HOTKotCheckTabless",
                column: "HotTabID");

            migrationBuilder.CreateIndex(
                name: "IX_HotKOTItemDetails_HotKOTId",
                table: "HotKOTItemDetails",
                column: "HotKOTId");

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
                name: "IX_StockTransferItems_StkTrId",
                table: "StockTransferItems",
                column: "StkTrId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_catId",
                table: "SubCategories",
                column: "catId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BarItemCounterStocks");

            migrationBuilder.DropTable(
                name: "BillItemUnits");

            migrationBuilder.DropTable(
                name: "BranchTaxSettings");

            migrationBuilder.DropTable(
                name: "DiningSpaceItemRates");

            migrationBuilder.DropTable(
                name: "HotBillAgainstKots");

            migrationBuilder.DropTable(
                name: "HotBillItemDetail");

            migrationBuilder.DropTable(
                name: "HotBillTaxDetails");

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
                name: "StockTransferItems");

            migrationBuilder.DropTable(
                name: "SyncHistories");

            migrationBuilder.DropTable(
                name: "TablesDiningSpaces");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "BarItems");

            migrationBuilder.DropTable(
                name: "BillStations");

            migrationBuilder.DropTable(
                name: "BillItems");

            migrationBuilder.DropTable(
                name: "DiningSpaces");

            migrationBuilder.DropTable(
                name: "HotBillMasters");

            migrationBuilder.DropTable(
                name: "TaxMasters");

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
                name: "StockTransfers");

            migrationBuilder.DropTable(
                name: "BranchMasters");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}

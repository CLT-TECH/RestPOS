using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class addmasters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MainItemType",
                table: "MainItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

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
                name: "DiningSpaces",
                columns: table => new
                {
                    diningSpaceId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    diningSpaceName = table.Column<string>(type: "TEXT", nullable: false),
                    branchId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiningSpaces", x => x.diningSpaceId);
                    table.ForeignKey(
                        name: "FK_DiningSpaces_BranchMasters_branchId",
                        column: x => x.branchId,
                        principalTable: "BranchMasters",
                        principalColumn: "branchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillStations_branchId",
                table: "BillStations",
                column: "branchId");

            migrationBuilder.CreateIndex(
                name: "IX_DiningSpaces_branchId",
                table: "DiningSpaces",
                column: "branchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillStations");

            migrationBuilder.DropTable(
                name: "DiningSpaces");

            migrationBuilder.DropTable(
                name: "BranchMasters");

            migrationBuilder.DropColumn(
                name: "MainItemType",
                table: "MainItems");
        }
    }
}

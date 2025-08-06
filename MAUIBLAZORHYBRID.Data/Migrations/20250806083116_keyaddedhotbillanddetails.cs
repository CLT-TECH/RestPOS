using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class keyaddedhotbillanddetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaxMaster",
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
                    table.PrimaryKey("PK_TaxMaster", x => x.TaxId);
                });

            migrationBuilder.CreateTable(
                name: "BranchTaxSettings",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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
                    table.ForeignKey(
                        name: "FK_BranchTaxSettings_TaxMaster_TaxId",
                        column: x => x.TaxId,
                        principalTable: "TaxMaster",
                        principalColumn: "TaxId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BranchTaxSettings_BranchId",
                table: "BranchTaxSettings",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchTaxSettings_TaxId",
                table: "BranchTaxSettings",
                column: "TaxId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BranchTaxSettings");

            migrationBuilder.DropTable(
                name: "TaxMaster");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class vendormaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VendorMasters",
                columns: table => new
                {
                    VendorId = table.Column<int>(type: "INTEGER", nullable: false),
                    VendorCode = table.Column<string>(type: "TEXT", nullable: false),
                    VendorName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorMasters", x => x.VendorId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VendorMasters");
        }
    }
}

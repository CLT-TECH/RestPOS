using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAUIBLAZORHYBRID.Data.Migrations
{
    /// <inheritdoc />
    public partial class migrationuniterroritemconnectionreremove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_MainItem_MainItemId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Unit_unitId",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Unit",
                table: "Unit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MainItem",
                table: "MainItem");

            migrationBuilder.RenameTable(
                name: "Unit",
                newName: "Units");

            migrationBuilder.RenameTable(
                name: "MainItem",
                newName: "MainItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Units",
                table: "Units",
                column: "unitId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MainItems",
                table: "MainItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_MainItems_MainItemId",
                table: "Items",
                column: "MainItemId",
                principalTable: "MainItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Units_unitId",
                table: "Items",
                column: "unitId",
                principalTable: "Units",
                principalColumn: "unitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_MainItems_MainItemId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Units_unitId",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Units",
                table: "Units");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MainItems",
                table: "MainItems");

            migrationBuilder.RenameTable(
                name: "Units",
                newName: "Unit");

            migrationBuilder.RenameTable(
                name: "MainItems",
                newName: "MainItem");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Unit",
                table: "Unit",
                column: "unitId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MainItem",
                table: "MainItem",
                column: "Id");

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
    }
}

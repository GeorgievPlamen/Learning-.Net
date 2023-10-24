using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class StocksDB_Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BuyOrders",
                table: "BuyOrders");

            migrationBuilder.RenameTable(
                name: "BuyOrders",
                newName: "BuyOders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BuyOders",
                table: "BuyOders",
                column: "BuyOrderID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BuyOders",
                table: "BuyOders");

            migrationBuilder.RenameTable(
                name: "BuyOders",
                newName: "BuyOrders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BuyOrders",
                table: "BuyOrders",
                column: "BuyOrderID");
        }
    }
}

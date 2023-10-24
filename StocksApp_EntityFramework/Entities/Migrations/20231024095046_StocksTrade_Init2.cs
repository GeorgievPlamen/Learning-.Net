using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class StocksTrade_Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BuyOders",
                columns: new[] { "BuyOrderID", "DateAndTimeOfOrder", "Price", "Quantity", "StockName", "StockSymbol" },
                values: new object[] { new Guid("96996e0f-a54c-4a45-afd7-e3ff98daab5f"), new DateTime(2007, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 30.0, 10L, "microsoft", "MSFT" });

            migrationBuilder.InsertData(
                table: "SellOrders",
                columns: new[] { "SellOrderID", "DateAndTimeOfOrder", "Price", "Quantity", "StockName", "StockSymbol" },
                values: new object[] { new Guid("658253c8-3211-4343-a0da-285d4e960203"), new DateTime(2007, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 30.0, 10L, "microsoft", "MSFT" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BuyOders",
                keyColumn: "BuyOrderID",
                keyValue: new Guid("96996e0f-a54c-4a45-afd7-e3ff98daab5f"));

            migrationBuilder.DeleteData(
                table: "SellOrders",
                keyColumn: "SellOrderID",
                keyValue: new Guid("658253c8-3211-4343-a0da-285d4e960203"));
        }
    }
}

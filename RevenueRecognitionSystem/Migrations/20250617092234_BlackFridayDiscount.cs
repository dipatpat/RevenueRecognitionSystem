using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RevenueRecognitionSystem.Migrations
{
    /// <inheritdoc />
    public partial class BlackFridayDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "EndDate", "Name", "Percentage", "StartDate", "Type" },
                values: new object[] { 3, new DateTime(2025, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Black Friday Upfront", 0.20m, new DateTime(2025, 11, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Upfront" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}

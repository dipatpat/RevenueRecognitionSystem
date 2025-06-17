using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RevenueRecognitionSystem.Migrations
{
    /// <inheritdoc />
    public partial class SoftwareSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Software",
                columns: new[] { "Id", "BaseUpfrontPrice", "Category", "Description", "IsAvailableAsSubscription", "IsAvailableAsUpfront", "Name", "Version" },
                values: new object[,]
                {
                    { 3, 0m, "Health", "CRM tool for growing health businesses", true, true, "SalesBooster", "2.0.0" },
                    { 4, 0m, "Health", "Medical records and appointment management", false, true, "MediSys", "1.3.0" },
                    { 5, 0m, "Technology", "DevOps and continuous delivery toolkit", true, true, "CodeFlow", "4.5.2" },
                    { 6, 0m, "Utilities", "Travel booking and itinerary management system", false, true, "TravelPro", "3.0.1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Software",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Software",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Software",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Software",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}

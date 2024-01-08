using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeclusterizationtypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClusterizationTypes",
                keyColumn: "Id",
                keyValue: "Channels");

            migrationBuilder.DeleteData(
                table: "ClusterizationTypes",
                keyColumn: "Id",
                keyValue: "Videos");

            migrationBuilder.InsertData(
                table: "ClusterizationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { "External", "З файлу" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClusterizationTypes",
                keyColumn: "Id",
                keyValue: "External");

            migrationBuilder.InsertData(
                table: "ClusterizationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "Channels", "Канали" },
                    { "Videos", "Відео" }
                });
        }
    }
}

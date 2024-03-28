using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addpackseeders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "QuotasPacks",
                column: "Id",
                values: new object[]
                {
                    1,
                    2
                });

            migrationBuilder.InsertData(
                table: "QuotasPackItems",
                columns: new[] { "Id", "Count", "PackId", "TypeId" },
                values: new object[,]
                {
                    { 1, 1000, 1, "Youtube" },
                    { 2, 1000, 1, "Embeddings" },
                    { 3, 10000, 1, "Clustering" },
                    { 4, 5, 1, "PublicWorkspaces" },
                    { 5, 20, 1, "PrivateWorkspaces" },
                    { 6, 20, 1, "PublicProfiles" },
                    { 7, 50, 1, "PrivateProfiles" },
                    { 8, 1000000000, 2, "Youtube" },
                    { 9, 1000000000, 2, "Embeddings" },
                    { 10, 1000000000, 2, "Clustering" },
                    { 11, 1000000000, 2, "PublicWorkspaces" },
                    { 12, 1000000000, 2, "PrivateWorkspaces" },
                    { 13, 1000000000, 2, "PublicProfiles" },
                    { 14, 1000000000, 2, "PrivateProfiles" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "QuotasPackItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "QuotasPackItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "QuotasPackItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "QuotasPackItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "QuotasPackItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "QuotasPackItems",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "QuotasPackItems",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "QuotasPackItems",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "QuotasPackItems",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "QuotasPackItems",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "QuotasPackItems",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "QuotasPackItems",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "QuotasPackItems",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "QuotasPackItems",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "QuotasPacks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "QuotasPacks",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addquotestypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "QuotesType",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { "Clustering", "Clusterization of data", "Clustering" },
                    { "Embeddings", "Creating embeddings", "Embeddings" },
                    { "PrivateProfiles", "Creating private profiles", "Private profiles" },
                    { "PrivateWorkspaces", "Creating private workspaces", "Private workspaces" },
                    { "PublicProfiles", "Creating public profiles", "Public profiles" },
                    { "PublicWorkspaces", "Creating public workspaces", "Public workspaces" },
                    { "Youtube", "Loading data from Youtube", "Youtube" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "QuotesType",
                keyColumn: "Id",
                keyValue: "Clustering");

            migrationBuilder.DeleteData(
                table: "QuotesType",
                keyColumn: "Id",
                keyValue: "Embeddings");

            migrationBuilder.DeleteData(
                table: "QuotesType",
                keyColumn: "Id",
                keyValue: "PrivateProfiles");

            migrationBuilder.DeleteData(
                table: "QuotesType",
                keyColumn: "Id",
                keyValue: "PrivateWorkspaces");

            migrationBuilder.DeleteData(
                table: "QuotesType",
                keyColumn: "Id",
                keyValue: "PublicProfiles");

            migrationBuilder.DeleteData(
                table: "QuotesType",
                keyColumn: "Id",
                keyValue: "PublicWorkspaces");

            migrationBuilder.DeleteData(
                table: "QuotesType",
                keyColumn: "Id",
                keyValue: "Youtube");
        }
    }
}

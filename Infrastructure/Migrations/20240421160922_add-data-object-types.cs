using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adddataobjecttypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MyDataObjectType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "Comment", "Comment" },
                    { "ExternalData", "External Data" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MyDataObjectType",
                keyColumn: "Id",
                keyValue: "Comment");

            migrationBuilder.DeleteData(
                table: "MyDataObjectType",
                keyColumn: "Id",
                keyValue: "ExternalData");
        }
    }
}

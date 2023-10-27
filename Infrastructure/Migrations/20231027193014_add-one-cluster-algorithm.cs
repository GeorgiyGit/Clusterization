using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addoneclusteralgorithm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClusterColor",
                table: "ClusterizationAbstractAlgorithms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "ClusterizationAlgorithmType",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { "OneCluster", "Об'єднання елементів в один кластер", "Один кластер" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClusterizationAlgorithmType",
                keyColumn: "Id",
                keyValue: "OneCluster");

            migrationBuilder.DropColumn(
                name: "ClusterColor",
                table: "ClusterizationAbstractAlgorithms");
        }
    }
}

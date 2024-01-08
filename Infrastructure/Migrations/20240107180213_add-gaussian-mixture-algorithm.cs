using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addgaussianmixturealgorithm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfComponents",
                table: "ClusterizationAbstractAlgorithms",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "ClusterizationAlgorithmType",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { "GaussianMixture", "Метод кластеризації, який моделює дані як суміш розділів Гауса", "Gaussian Mixture" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClusterizationAlgorithmType",
                keyColumn: "Id",
                keyValue: "GaussianMixture");

            migrationBuilder.DropColumn(
                name: "NumberOfComponents",
                table: "ClusterizationAbstractAlgorithms");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adddbscanalgorithm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Epsilon",
                table: "ClusterizationAbstractAlgorithms",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinimumPointsPerCluster",
                table: "ClusterizationAbstractAlgorithms",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Epsilon",
                table: "ClusterizationAbstractAlgorithms");

            migrationBuilder.DropColumn(
                name: "MinimumPointsPerCluster",
                table: "ClusterizationAbstractAlgorithms");
        }
    }
}

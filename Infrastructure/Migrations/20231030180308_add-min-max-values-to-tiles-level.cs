using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addminmaxvaluestotileslevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "MaxXValue",
                table: "ClusterizationTilesLevels",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MaxYValue",
                table: "ClusterizationTilesLevels",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MinXValue",
                table: "ClusterizationTilesLevels",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MinYValue",
                table: "ClusterizationTilesLevels",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxXValue",
                table: "ClusterizationTilesLevels");

            migrationBuilder.DropColumn(
                name: "MaxYValue",
                table: "ClusterizationTilesLevels");

            migrationBuilder.DropColumn(
                name: "MinXValue",
                table: "ClusterizationTilesLevels");

            migrationBuilder.DropColumn(
                name: "MinYValue",
                table: "ClusterizationTilesLevels");
        }
    }
}

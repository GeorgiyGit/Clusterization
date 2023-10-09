using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class profilechanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCalculated",
                table: "ClusterizationProfiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFullyCalculated",
                table: "ClusterizationProfiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MaxTileLevel",
                table: "ClusterizationProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinTileLevel",
                table: "ClusterizationProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCalculated",
                table: "ClusterizationProfiles");

            migrationBuilder.DropColumn(
                name: "IsFullyCalculated",
                table: "ClusterizationProfiles");

            migrationBuilder.DropColumn(
                name: "MaxTileLevel",
                table: "ClusterizationProfiles");

            migrationBuilder.DropColumn(
                name: "MinTileLevel",
                table: "ClusterizationProfiles");
        }
    }
}

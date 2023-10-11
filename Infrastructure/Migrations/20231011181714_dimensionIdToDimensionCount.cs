using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dimensionIdToDimensionCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClusterizationProfiles_ClusterizationDimensionTypes_DimensionTypeId",
                table: "ClusterizationProfiles");

            migrationBuilder.RenameColumn(
                name: "DimensionTypeId",
                table: "ClusterizationProfiles",
                newName: "DimensionCount");

            migrationBuilder.RenameIndex(
                name: "IX_ClusterizationProfiles_DimensionTypeId",
                table: "ClusterizationProfiles",
                newName: "IX_ClusterizationProfiles_DimensionCount");

            migrationBuilder.AddForeignKey(
                name: "FK_ClusterizationProfiles_ClusterizationDimensionTypes_DimensionCount",
                table: "ClusterizationProfiles",
                column: "DimensionCount",
                principalTable: "ClusterizationDimensionTypes",
                principalColumn: "DimensionCount",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClusterizationProfiles_ClusterizationDimensionTypes_DimensionCount",
                table: "ClusterizationProfiles");

            migrationBuilder.RenameColumn(
                name: "DimensionCount",
                table: "ClusterizationProfiles",
                newName: "DimensionTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ClusterizationProfiles_DimensionCount",
                table: "ClusterizationProfiles",
                newName: "IX_ClusterizationProfiles_DimensionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClusterizationProfiles_ClusterizationDimensionTypes_DimensionTypeId",
                table: "ClusterizationProfiles",
                column: "DimensionTypeId",
                principalTable: "ClusterizationDimensionTypes",
                principalColumn: "DimensionCount",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

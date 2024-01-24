using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addindexesfordrV : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DimensionalityReductionValues_TechniqueId",
                table: "DimensionalityReductionValues");

            migrationBuilder.CreateIndex(
                name: "IX_DimensionalityReductionValues_TechniqueId_ClusterizationWorkspaceDRTechniqueId",
                table: "DimensionalityReductionValues",
                columns: new[] { "TechniqueId", "ClusterizationWorkspaceDRTechniqueId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DimensionalityReductionValues_TechniqueId_ClusterizationWorkspaceDRTechniqueId",
                table: "DimensionalityReductionValues");

            migrationBuilder.CreateIndex(
                name: "IX_DimensionalityReductionValues_TechniqueId",
                table: "DimensionalityReductionValues",
                column: "TechniqueId");
        }
    }
}

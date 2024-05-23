using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addindexesforclusteralgorithms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationAbstractAlgorithms_Epsilon",
                table: "ClusterizationAbstractAlgorithms",
                column: "Epsilon");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationAbstractAlgorithms_NumberOfComponents",
                table: "ClusterizationAbstractAlgorithms",
                column: "NumberOfComponents");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationAbstractAlgorithms_NumClusters",
                table: "ClusterizationAbstractAlgorithms",
                column: "NumClusters");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationAbstractAlgorithms_SpectralClusteringAlgorithm_NumClusters",
                table: "ClusterizationAbstractAlgorithms",
                column: "SpectralClusteringAlgorithm_NumClusters");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClusterizationAbstractAlgorithms_Epsilon",
                table: "ClusterizationAbstractAlgorithms");

            migrationBuilder.DropIndex(
                name: "IX_ClusterizationAbstractAlgorithms_NumberOfComponents",
                table: "ClusterizationAbstractAlgorithms");

            migrationBuilder.DropIndex(
                name: "IX_ClusterizationAbstractAlgorithms_NumClusters",
                table: "ClusterizationAbstractAlgorithms");

            migrationBuilder.DropIndex(
                name: "IX_ClusterizationAbstractAlgorithms_SpectralClusteringAlgorithm_NumClusters",
                table: "ClusterizationAbstractAlgorithms");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addspectralclusteringalgorithm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Gamma",
                table: "ClusterizationAbstractAlgorithms",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpectralClusteringAlgorithm_NumClusters",
                table: "ClusterizationAbstractAlgorithms",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "ClusterizationAlgorithmType",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { "SpectralClustering", "Cпектральна кластеризація базується на принципах теорії графів і лінійної алгебри", "Spectral Clustering" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClusterizationAlgorithmType",
                keyColumn: "Id",
                keyValue: "SpectralClustering");

            migrationBuilder.DropColumn(
                name: "Gamma",
                table: "ClusterizationAbstractAlgorithms");

            migrationBuilder.DropColumn(
                name: "SpectralClusteringAlgorithm_NumClusters",
                table: "ClusterizationAbstractAlgorithms");
        }
    }
}

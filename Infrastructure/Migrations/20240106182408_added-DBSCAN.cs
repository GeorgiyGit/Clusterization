using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedDBSCAN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ClusterizationAlgorithmType",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { "DBScan", "Density-Based Spatial Clustering Of Applications With Noise", "DBSCAN" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClusterizationAlgorithmType",
                keyColumn: "Id",
                keyValue: "DBScan");
        }
    }
}

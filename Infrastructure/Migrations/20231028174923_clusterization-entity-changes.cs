using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class clusterizationentitychanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClusterizationEntites_DisplayedPoints_DisplayedPointId",
                table: "ClusterizationEntites");

            migrationBuilder.DropIndex(
                name: "IX_ClusterizationEntites_DisplayedPointId",
                table: "ClusterizationEntites");

            migrationBuilder.DropColumn(
                name: "ClusterizationEntityId",
                table: "DisplayedPoints");

            migrationBuilder.DropColumn(
                name: "DisplayedPointId",
                table: "ClusterizationEntites");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClusterizationEntityId",
                table: "DisplayedPoints",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DisplayedPointId",
                table: "ClusterizationEntites",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationEntites_DisplayedPointId",
                table: "ClusterizationEntites",
                column: "DisplayedPointId",
                unique: true,
                filter: "[DisplayedPointId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ClusterizationEntites_DisplayedPoints_DisplayedPointId",
                table: "ClusterizationEntites",
                column: "DisplayedPointId",
                principalTable: "DisplayedPoints",
                principalColumn: "Id");
        }
    }
}

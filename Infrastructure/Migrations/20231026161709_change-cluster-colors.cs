using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeclustercolors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clusters_ClusterizationColorValues_ColorId",
                table: "Clusters");

            migrationBuilder.DropTable(
                name: "ClusterizationColorValues");

            migrationBuilder.DropTable(
                name: "ClusterizationPointColors");

            migrationBuilder.DropIndex(
                name: "IX_Clusters_ColorId",
                table: "Clusters");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Clusters");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Clusters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ParentClusterId",
                table: "Clusters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clusters_ParentClusterId",
                table: "Clusters",
                column: "ParentClusterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clusters_Clusters_ParentClusterId",
                table: "Clusters",
                column: "ParentClusterId",
                principalTable: "Clusters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clusters_Clusters_ParentClusterId",
                table: "Clusters");

            migrationBuilder.DropIndex(
                name: "IX_Clusters_ParentClusterId",
                table: "Clusters");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Clusters");

            migrationBuilder.DropColumn(
                name: "ParentClusterId",
                table: "Clusters");

            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "Clusters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ClusterizationPointColors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PointId = table.Column<int>(type: "int", nullable: false),
                    ProfileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterizationPointColors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClusterizationPointColors_ClusterizationProfiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "ClusterizationProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClusterizationPointColors_DisplayedPoints_PointId",
                        column: x => x.PointId,
                        principalTable: "DisplayedPoints",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClusterizationColorValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PointColorsId = table.Column<int>(type: "int", nullable: true),
                    ClusterId = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterizationColorValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClusterizationColorValues_ClusterizationPointColors_PointColorsId",
                        column: x => x.PointColorsId,
                        principalTable: "ClusterizationPointColors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clusters_ColorId",
                table: "Clusters",
                column: "ColorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationColorValues_PointColorsId",
                table: "ClusterizationColorValues",
                column: "PointColorsId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationPointColors_PointId",
                table: "ClusterizationPointColors",
                column: "PointId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationPointColors_ProfileId",
                table: "ClusterizationPointColors",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clusters_ClusterizationColorValues_ColorId",
                table: "Clusters",
                column: "ColorId",
                principalTable: "ClusterizationColorValues",
                principalColumn: "Id");
        }
    }
}

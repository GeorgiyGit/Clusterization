using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addtileslevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TilesLevelId",
                table: "ClusterizationTiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ClusterizationTilesLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X = table.Column<int>(type: "int", nullable: false),
                    TileLength = table.Column<double>(type: "float", nullable: false),
                    TileCount = table.Column<int>(type: "int", nullable: false),
                    ProfileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterizationTilesLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClusterizationTilesLevels_ClusterizationProfiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "ClusterizationProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationTiles_TilesLevelId",
                table: "ClusterizationTiles",
                column: "TilesLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationTilesLevels_ProfileId",
                table: "ClusterizationTilesLevels",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClusterizationTiles_ClusterizationTilesLevels_TilesLevelId",
                table: "ClusterizationTiles",
                column: "TilesLevelId",
                principalTable: "ClusterizationTilesLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClusterizationTiles_ClusterizationTilesLevels_TilesLevelId",
                table: "ClusterizationTiles");

            migrationBuilder.DropTable(
                name: "ClusterizationTilesLevels");

            migrationBuilder.DropIndex(
                name: "IX_ClusterizationTiles_TilesLevelId",
                table: "ClusterizationTiles");

            migrationBuilder.DropColumn(
                name: "TilesLevelId",
                table: "ClusterizationTiles");
        }
    }
}

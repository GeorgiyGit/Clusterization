using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addClusterizationWorkspaceDRTechniques : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClusterizationWorkspaceDRTechniqueId",
                table: "DimensionalityReductionValues",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClusterizationWorkspaceDRTechniques",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DRTechniqueId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkspaceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterizationWorkspaceDRTechniques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClusterizationWorkspaceDRTechniques_ClusterizationWorkspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalTable: "ClusterizationWorkspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClusterizationWorkspaceDRTechniques_DimensionalityReductionTechniques_DRTechniqueId",
                        column: x => x.DRTechniqueId,
                        principalTable: "DimensionalityReductionTechniques",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DimensionalityReductionValues_ClusterizationWorkspaceDRTechniqueId",
                table: "DimensionalityReductionValues",
                column: "ClusterizationWorkspaceDRTechniqueId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationWorkspaceDRTechniques_DRTechniqueId",
                table: "ClusterizationWorkspaceDRTechniques",
                column: "DRTechniqueId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationWorkspaceDRTechniques_WorkspaceId",
                table: "ClusterizationWorkspaceDRTechniques",
                column: "WorkspaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_DimensionalityReductionValues_ClusterizationWorkspaceDRTechniques_ClusterizationWorkspaceDRTechniqueId",
                table: "DimensionalityReductionValues",
                column: "ClusterizationWorkspaceDRTechniqueId",
                principalTable: "ClusterizationWorkspaceDRTechniques",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DimensionalityReductionValues_ClusterizationWorkspaceDRTechniques_ClusterizationWorkspaceDRTechniqueId",
                table: "DimensionalityReductionValues");

            migrationBuilder.DropTable(
                name: "ClusterizationWorkspaceDRTechniques");

            migrationBuilder.DropIndex(
                name: "IX_DimensionalityReductionValues_ClusterizationWorkspaceDRTechniqueId",
                table: "DimensionalityReductionValues");

            migrationBuilder.DropColumn(
                name: "ClusterizationWorkspaceDRTechniqueId",
                table: "DimensionalityReductionValues");
        }
    }
}

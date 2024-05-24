using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addfastclusteringworkflows : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FastClusteringWorkflowId",
                table: "ClusterizationWorkspaces",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FastClusteringWorkflowId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FastClusteringWorkflows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FastClusteringWorkflows", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationWorkspaces_FastClusteringWorkflowId",
                table: "ClusterizationWorkspaces",
                column: "FastClusteringWorkflowId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FastClusteringWorkflowId",
                table: "AspNetUsers",
                column: "FastClusteringWorkflowId",
                unique: true,
                filter: "[FastClusteringWorkflowId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_FastClusteringWorkflows_FastClusteringWorkflowId",
                table: "AspNetUsers",
                column: "FastClusteringWorkflowId",
                principalTable: "FastClusteringWorkflows",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClusterizationWorkspaces_FastClusteringWorkflows_FastClusteringWorkflowId",
                table: "ClusterizationWorkspaces",
                column: "FastClusteringWorkflowId",
                principalTable: "FastClusteringWorkflows",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FastClusteringWorkflows_FastClusteringWorkflowId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ClusterizationWorkspaces_FastClusteringWorkflows_FastClusteringWorkflowId",
                table: "ClusterizationWorkspaces");

            migrationBuilder.DropTable(
                name: "FastClusteringWorkflows");

            migrationBuilder.DropIndex(
                name: "IX_ClusterizationWorkspaces_FastClusteringWorkflowId",
                table: "ClusterizationWorkspaces");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FastClusteringWorkflowId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FastClusteringWorkflowId",
                table: "ClusterizationWorkspaces");

            migrationBuilder.DropColumn(
                name: "FastClusteringWorkflowId",
                table: "AspNetUsers");
        }
    }
}

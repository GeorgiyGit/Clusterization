using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addexternalobject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClusterizationEntites_Comments_CommentId",
                table: "ClusterizationEntites");

            migrationBuilder.AddColumn<string>(
                name: "ExternalObjectId",
                table: "EmbeddingDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CommentId",
                table: "ClusterizationEntites",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ExternalObjectId",
                table: "ClusterizationEntites",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExternalObjects",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Sesion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmbeddingDataId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalObjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalObjects_EmbeddingDatas_EmbeddingDataId",
                        column: x => x.EmbeddingDataId,
                        principalTable: "EmbeddingDatas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClusterizationWorkspaceExternalObject",
                columns: table => new
                {
                    ExternalObjectsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkspacesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterizationWorkspaceExternalObject", x => new { x.ExternalObjectsId, x.WorkspacesId });
                    table.ForeignKey(
                        name: "FK_ClusterizationWorkspaceExternalObject_ClusterizationWorkspaces_WorkspacesId",
                        column: x => x.WorkspacesId,
                        principalTable: "ClusterizationWorkspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClusterizationWorkspaceExternalObject_ExternalObjects_ExternalObjectsId",
                        column: x => x.ExternalObjectsId,
                        principalTable: "ExternalObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationEntites_ExternalObjectId",
                table: "ClusterizationEntites",
                column: "ExternalObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationWorkspaceExternalObject_WorkspacesId",
                table: "ClusterizationWorkspaceExternalObject",
                column: "WorkspacesId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalObjects_EmbeddingDataId",
                table: "ExternalObjects",
                column: "EmbeddingDataId",
                unique: true,
                filter: "[EmbeddingDataId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ClusterizationEntites_Comments_CommentId",
                table: "ClusterizationEntites",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClusterizationEntites_ExternalObjects_ExternalObjectId",
                table: "ClusterizationEntites",
                column: "ExternalObjectId",
                principalTable: "ExternalObjects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClusterizationEntites_Comments_CommentId",
                table: "ClusterizationEntites");

            migrationBuilder.DropForeignKey(
                name: "FK_ClusterizationEntites_ExternalObjects_ExternalObjectId",
                table: "ClusterizationEntites");

            migrationBuilder.DropTable(
                name: "ClusterizationWorkspaceExternalObject");

            migrationBuilder.DropTable(
                name: "ExternalObjects");

            migrationBuilder.DropIndex(
                name: "IX_ClusterizationEntites_ExternalObjectId",
                table: "ClusterizationEntites");

            migrationBuilder.DropColumn(
                name: "ExternalObjectId",
                table: "EmbeddingDatas");

            migrationBuilder.DropColumn(
                name: "ExternalObjectId",
                table: "ClusterizationEntites");

            migrationBuilder.AlterColumn<string>(
                name: "CommentId",
                table: "ClusterizationEntites",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ClusterizationEntites_Comments_CommentId",
                table: "ClusterizationEntites",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addfullid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClusterizationEntites_ExternalObjects_ExternalObjectId",
                table: "ClusterizationEntites");

            migrationBuilder.DropForeignKey(
                name: "FK_ClusterizationWorkspaceExternalObject_ExternalObjects_ExternalObjectsId",
                table: "ClusterizationWorkspaceExternalObject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExternalObjects",
                table: "ExternalObjects");

            migrationBuilder.RenameColumn(
                name: "ExternalObjectsId",
                table: "ClusterizationWorkspaceExternalObject",
                newName: "ExternalObjectsFullId");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "ExternalObjects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "FullId",
                table: "ExternalObjects",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExternalObjects",
                table: "ExternalObjects",
                column: "FullId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClusterizationEntites_ExternalObjects_ExternalObjectId",
                table: "ClusterizationEntites",
                column: "ExternalObjectId",
                principalTable: "ExternalObjects",
                principalColumn: "FullId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClusterizationWorkspaceExternalObject_ExternalObjects_ExternalObjectsFullId",
                table: "ClusterizationWorkspaceExternalObject",
                column: "ExternalObjectsFullId",
                principalTable: "ExternalObjects",
                principalColumn: "FullId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClusterizationEntites_ExternalObjects_ExternalObjectId",
                table: "ClusterizationEntites");

            migrationBuilder.DropForeignKey(
                name: "FK_ClusterizationWorkspaceExternalObject_ExternalObjects_ExternalObjectsFullId",
                table: "ClusterizationWorkspaceExternalObject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExternalObjects",
                table: "ExternalObjects");

            migrationBuilder.DropColumn(
                name: "FullId",
                table: "ExternalObjects");

            migrationBuilder.RenameColumn(
                name: "ExternalObjectsFullId",
                table: "ClusterizationWorkspaceExternalObject",
                newName: "ExternalObjectsId");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "ExternalObjects",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExternalObjects",
                table: "ExternalObjects",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClusterizationEntites_ExternalObjects_ExternalObjectId",
                table: "ClusterizationEntites",
                column: "ExternalObjectId",
                principalTable: "ExternalObjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClusterizationWorkspaceExternalObject_ExternalObjects_ExternalObjectsId",
                table: "ClusterizationWorkspaceExternalObject",
                column: "ExternalObjectsId",
                principalTable: "ExternalObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

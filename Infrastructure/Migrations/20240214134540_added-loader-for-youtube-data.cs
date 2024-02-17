using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedloaderforyoutubedata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClusterizationProfiles_Customers_OwnerId",
                table: "ClusterizationProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_ClusterizationWorkspaces_Customers_OwnerId",
                table: "ClusterizationWorkspaces");

            migrationBuilder.AddColumn<string>(
                name: "LoaderId",
                table: "Videos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LoaderId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "ClusterizationWorkspaces",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "ClusterizationProfiles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "ClusterizationAbstractAlgorithms",
                type: "nvarchar(34)",
                maxLength: 34,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "LoaderId",
                table: "Channels",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_LoaderId",
                table: "Videos",
                column: "LoaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_LoaderId",
                table: "Comments",
                column: "LoaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Channels_LoaderId",
                table: "Channels",
                column: "LoaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Channels_Customers_LoaderId",
                table: "Channels",
                column: "LoaderId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClusterizationProfiles_Customers_OwnerId",
                table: "ClusterizationProfiles",
                column: "OwnerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClusterizationWorkspaces_Customers_OwnerId",
                table: "ClusterizationWorkspaces",
                column: "OwnerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Customers_LoaderId",
                table: "Comments",
                column: "LoaderId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Customers_LoaderId",
                table: "Videos",
                column: "LoaderId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Channels_Customers_LoaderId",
                table: "Channels");

            migrationBuilder.DropForeignKey(
                name: "FK_ClusterizationProfiles_Customers_OwnerId",
                table: "ClusterizationProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_ClusterizationWorkspaces_Customers_OwnerId",
                table: "ClusterizationWorkspaces");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Customers_LoaderId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Customers_LoaderId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_LoaderId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Comments_LoaderId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Channels_LoaderId",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "LoaderId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "LoaderId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "LoaderId",
                table: "Channels");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "ClusterizationWorkspaces",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "ClusterizationProfiles",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "ClusterizationAbstractAlgorithms",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(34)",
                oldMaxLength: 34);

            migrationBuilder.AddForeignKey(
                name: "FK_ClusterizationProfiles_Customers_OwnerId",
                table: "ClusterizationProfiles",
                column: "OwnerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClusterizationWorkspaces_Customers_OwnerId",
                table: "ClusterizationWorkspaces",
                column: "OwnerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}

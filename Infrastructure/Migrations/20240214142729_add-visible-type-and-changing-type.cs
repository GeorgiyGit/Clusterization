using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addvisibletypeandchangingtype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChangingType",
                table: "ClusterizationWorkspaces",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ChangingType",
                table: "ClusterizationProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VisibleType",
                table: "ClusterizationProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangingType",
                table: "ClusterizationWorkspaces");

            migrationBuilder.DropColumn(
                name: "ChangingType",
                table: "ClusterizationProfiles");

            migrationBuilder.DropColumn(
                name: "VisibleType",
                table: "ClusterizationProfiles");
        }
    }
}

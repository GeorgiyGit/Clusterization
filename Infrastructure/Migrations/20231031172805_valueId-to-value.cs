using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class valueIdtovalue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValueId",
                table: "DisplayedPoints",
                newName: "Value");

            migrationBuilder.AddColumn<string>(
                name: "TextValue",
                table: "ClusterizationEntites",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TextValue",
                table: "ClusterizationEntites");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "DisplayedPoints",
                newName: "ValueId");
        }
    }
}

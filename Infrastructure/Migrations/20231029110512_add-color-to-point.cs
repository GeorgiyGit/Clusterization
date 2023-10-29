using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addcolortopoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClusterId",
                table: "DisplayedPoints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DisplayedPoints_ClusterId",
                table: "DisplayedPoints",
                column: "ClusterId");

            migrationBuilder.AddForeignKey(
                name: "FK_DisplayedPoints_Clusters_ClusterId",
                table: "DisplayedPoints",
                column: "ClusterId",
                principalTable: "Clusters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DisplayedPoints_Clusters_ClusterId",
                table: "DisplayedPoints");

            migrationBuilder.DropIndex(
                name: "IX_DisplayedPoints_ClusterId",
                table: "DisplayedPoints");

            migrationBuilder.DropColumn(
                name: "ClusterId",
                table: "DisplayedPoints");
        }
    }
}

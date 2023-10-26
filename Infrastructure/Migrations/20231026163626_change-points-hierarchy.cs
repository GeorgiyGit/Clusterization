using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changepointshierarchy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DisplayedPoints_DisplayedPoints_ParentPointId",
                table: "DisplayedPoints");

            migrationBuilder.DropIndex(
                name: "IX_DisplayedPoints_ParentPointId",
                table: "DisplayedPoints");

            migrationBuilder.DropColumn(
                name: "ParentPointId",
                table: "DisplayedPoints");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentPointId",
                table: "DisplayedPoints",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DisplayedPoints_ParentPointId",
                table: "DisplayedPoints",
                column: "ParentPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_DisplayedPoints_DisplayedPoints_ParentPointId",
                table: "DisplayedPoints",
                column: "ParentPointId",
                principalTable: "DisplayedPoints",
                principalColumn: "Id");
        }
    }
}

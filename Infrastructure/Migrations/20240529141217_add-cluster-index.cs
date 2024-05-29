using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addclusterindex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Clusters_ChildElementsCount",
                table: "Clusters");

            migrationBuilder.CreateIndex(
                name: "IX_Clusters_ChildElementsCount_ProfileId",
                table: "Clusters",
                columns: new[] { "ChildElementsCount", "ProfileId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Clusters_ChildElementsCount_ProfileId",
                table: "Clusters");

            migrationBuilder.CreateIndex(
                name: "IX_Clusters_ChildElementsCount",
                table: "Clusters",
                column: "ChildElementsCount");
        }
    }
}

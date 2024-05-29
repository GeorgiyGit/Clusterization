using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeindexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Clusters_ChildElementsCount_ProfileId",
                table: "Clusters");

            migrationBuilder.CreateIndex(
                name: "IX_Clusters_ChildElementsCount",
                table: "Clusters",
                column: "ChildElementsCount",
                descending: new bool[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Clusters_ChildElementsCount",
                table: "Clusters");

            migrationBuilder.CreateIndex(
                name: "IX_Clusters_ChildElementsCount_ProfileId",
                table: "Clusters",
                columns: new[] { "ChildElementsCount", "ProfileId" });
        }
    }
}

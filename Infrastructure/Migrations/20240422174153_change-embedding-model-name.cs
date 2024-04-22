using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeembeddingmodelname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmbeddingModels",
                keyColumn: "Id",
                keyValue: "text-embedding-3-small",
                column: "Name",
                value: "text_embedding_3_small");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmbeddingModels",
                keyColumn: "Id",
                keyValue: "text-embedding-3-small",
                column: "Name",
                value: "text-embedding-ada-002");
        }
    }
}

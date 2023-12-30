using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changePointsRepresentation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmbeddingValues");

            migrationBuilder.DeleteData(
                table: "DimensionalityReductionTechniques",
                keyColumn: "Id",
                keyValue: "LLE");

            migrationBuilder.AddColumn<string>(
                name: "ValuesString",
                table: "EmbeddingDimensionValues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValuesString",
                table: "EmbeddingDimensionValues");

            migrationBuilder.CreateTable(
                name: "EmbeddingValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmbeddingDimensionValueId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmbeddingValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmbeddingValues_EmbeddingDimensionValues_EmbeddingDimensionValueId",
                        column: x => x.EmbeddingDimensionValueId,
                        principalTable: "EmbeddingDimensionValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DimensionalityReductionTechniques",
                columns: new[] { "Id", "Name" },
                values: new object[] { "LLE", "Locally Linear Embedding" });

            migrationBuilder.CreateIndex(
                name: "IX_EmbeddingValues_EmbeddingDimensionValueId",
                table: "EmbeddingValues",
                column: "EmbeddingDimensionValueId");
        }
    }
}

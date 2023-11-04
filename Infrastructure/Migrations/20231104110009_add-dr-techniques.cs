using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adddrtechniques : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmbeddingDimensionValues_EmbeddingDatas_EmbeddingDataId",
                table: "EmbeddingDimensionValues");

            migrationBuilder.DropIndex(
                name: "IX_EmbeddingDimensionValues_EmbeddingDataId",
                table: "EmbeddingDimensionValues");

            migrationBuilder.AlterColumn<int>(
                name: "EmbeddingDataId",
                table: "EmbeddingDimensionValues",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DimensionalityReductionValueId",
                table: "EmbeddingDimensionValues",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DimensionalityReductionValueId",
                table: "EmbeddingDatas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OriginalEmbeddingId",
                table: "EmbeddingDatas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DimensionalityReductionTechniqueId",
                table: "ClusterizationProfiles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DimensionalityReductionTechniques",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimensionalityReductionTechniques", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DimensionalityReductionValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmbeddingDataId = table.Column<int>(type: "int", nullable: true),
                    ClusterizationEntityId = table.Column<int>(type: "int", nullable: true),
                    TechniqueId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimensionalityReductionValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DimensionalityReductionValues_ClusterizationEntites_ClusterizationEntityId",
                        column: x => x.ClusterizationEntityId,
                        principalTable: "ClusterizationEntites",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DimensionalityReductionValues_DimensionalityReductionTechniques_TechniqueId",
                        column: x => x.TechniqueId,
                        principalTable: "DimensionalityReductionTechniques",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DimensionalityReductionValues_EmbeddingDatas_EmbeddingDataId",
                        column: x => x.EmbeddingDataId,
                        principalTable: "EmbeddingDatas",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "DimensionalityReductionTechniques",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "JSL", "Johnson-Lindenstrauss lemma" },
                    { "LLE", "Locally Linear Embedding" },
                    { "PCA", "Principal Component Analysis" },
                    { "t-SNE", "t-Distributed Stochastic Neighbor Embedding" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmbeddingDimensionValues_DimensionalityReductionValueId",
                table: "EmbeddingDimensionValues",
                column: "DimensionalityReductionValueId");

            migrationBuilder.CreateIndex(
                name: "IX_EmbeddingDimensionValues_EmbeddingDataId",
                table: "EmbeddingDimensionValues",
                column: "EmbeddingDataId",
                unique: true,
                filter: "[EmbeddingDataId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationProfiles_DimensionalityReductionTechniqueId",
                table: "ClusterizationProfiles",
                column: "DimensionalityReductionTechniqueId");

            migrationBuilder.CreateIndex(
                name: "IX_DimensionalityReductionValues_ClusterizationEntityId",
                table: "DimensionalityReductionValues",
                column: "ClusterizationEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_DimensionalityReductionValues_EmbeddingDataId",
                table: "DimensionalityReductionValues",
                column: "EmbeddingDataId",
                unique: true,
                filter: "[EmbeddingDataId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DimensionalityReductionValues_TechniqueId",
                table: "DimensionalityReductionValues",
                column: "TechniqueId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClusterizationProfiles_DimensionalityReductionTechniques_DimensionalityReductionTechniqueId",
                table: "ClusterizationProfiles",
                column: "DimensionalityReductionTechniqueId",
                principalTable: "DimensionalityReductionTechniques",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmbeddingDimensionValues_DimensionalityReductionValues_DimensionalityReductionValueId",
                table: "EmbeddingDimensionValues",
                column: "DimensionalityReductionValueId",
                principalTable: "DimensionalityReductionValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmbeddingDimensionValues_EmbeddingDatas_EmbeddingDataId",
                table: "EmbeddingDimensionValues",
                column: "EmbeddingDataId",
                principalTable: "EmbeddingDatas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClusterizationProfiles_DimensionalityReductionTechniques_DimensionalityReductionTechniqueId",
                table: "ClusterizationProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_EmbeddingDimensionValues_DimensionalityReductionValues_DimensionalityReductionValueId",
                table: "EmbeddingDimensionValues");

            migrationBuilder.DropForeignKey(
                name: "FK_EmbeddingDimensionValues_EmbeddingDatas_EmbeddingDataId",
                table: "EmbeddingDimensionValues");

            migrationBuilder.DropTable(
                name: "DimensionalityReductionValues");

            migrationBuilder.DropTable(
                name: "DimensionalityReductionTechniques");

            migrationBuilder.DropIndex(
                name: "IX_EmbeddingDimensionValues_DimensionalityReductionValueId",
                table: "EmbeddingDimensionValues");

            migrationBuilder.DropIndex(
                name: "IX_EmbeddingDimensionValues_EmbeddingDataId",
                table: "EmbeddingDimensionValues");

            migrationBuilder.DropIndex(
                name: "IX_ClusterizationProfiles_DimensionalityReductionTechniqueId",
                table: "ClusterizationProfiles");

            migrationBuilder.DropColumn(
                name: "DimensionalityReductionValueId",
                table: "EmbeddingDimensionValues");

            migrationBuilder.DropColumn(
                name: "DimensionalityReductionValueId",
                table: "EmbeddingDatas");

            migrationBuilder.DropColumn(
                name: "OriginalEmbeddingId",
                table: "EmbeddingDatas");

            migrationBuilder.DropColumn(
                name: "DimensionalityReductionTechniqueId",
                table: "ClusterizationProfiles");

            migrationBuilder.AlterColumn<int>(
                name: "EmbeddingDataId",
                table: "EmbeddingDimensionValues",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmbeddingDimensionValues_EmbeddingDataId",
                table: "EmbeddingDimensionValues",
                column: "EmbeddingDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmbeddingDimensionValues_EmbeddingDatas_EmbeddingDataId",
                table: "EmbeddingDimensionValues",
                column: "EmbeddingDataId",
                principalTable: "EmbeddingDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

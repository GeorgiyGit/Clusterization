using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addquotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DimensionalityReductionValues_EmbeddingDatas_EmbeddingDataId",
                table: "DimensionalityReductionValues");

            migrationBuilder.DropIndex(
                name: "IX_DimensionalityReductionValues_EmbeddingDataId",
                table: "DimensionalityReductionValues");

            migrationBuilder.CreateTable(
                name: "QuotesType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotesType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerQuotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpiredCount = table.Column<int>(type: "int", nullable: false),
                    AvailableCount = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerQuotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerQuotes_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerQuotes_QuotesType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "QuotesType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuotesLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotesLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuotesLogs_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuotesLogs_QuotesType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "QuotesType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmbeddingDatas_DimensionalityReductionValueId",
                table: "EmbeddingDatas",
                column: "DimensionalityReductionValueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerQuotes_CustomerId",
                table: "CustomerQuotes",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerQuotes_TypeId",
                table: "CustomerQuotes",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotesLogs_CustomerId",
                table: "QuotesLogs",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotesLogs_TypeId",
                table: "QuotesLogs",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmbeddingDatas_DimensionalityReductionValues_DimensionalityReductionValueId",
                table: "EmbeddingDatas",
                column: "DimensionalityReductionValueId",
                principalTable: "DimensionalityReductionValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmbeddingDatas_DimensionalityReductionValues_DimensionalityReductionValueId",
                table: "EmbeddingDatas");

            migrationBuilder.DropTable(
                name: "CustomerQuotes");

            migrationBuilder.DropTable(
                name: "QuotesLogs");

            migrationBuilder.DropTable(
                name: "QuotesType");

            migrationBuilder.DropIndex(
                name: "IX_EmbeddingDatas_DimensionalityReductionValueId",
                table: "EmbeddingDatas");

            migrationBuilder.CreateIndex(
                name: "IX_DimensionalityReductionValues_EmbeddingDataId",
                table: "DimensionalityReductionValues",
                column: "EmbeddingDataId",
                unique: true,
                filter: "[EmbeddingDataId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_DimensionalityReductionValues_EmbeddingDatas_EmbeddingDataId",
                table: "DimensionalityReductionValues",
                column: "EmbeddingDataId",
                principalTable: "EmbeddingDatas",
                principalColumn: "Id");
        }
    }
}

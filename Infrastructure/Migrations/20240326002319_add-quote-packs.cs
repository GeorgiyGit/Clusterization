using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addquotepacks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerQuotes_QuotesType_TypeId",
                table: "CustomerQuotes");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotesLogs_QuotesType_TypeId",
                table: "QuotesLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuotesType",
                table: "QuotesType");

            migrationBuilder.RenameTable(
                name: "QuotesType",
                newName: "QuotesTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuotesTypes",
                table: "QuotesTypes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "QuotesPacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotesPacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuotesPackItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PackId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotesPackItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuotesPackItems_QuotesPacks_PackId",
                        column: x => x.PackId,
                        principalTable: "QuotesPacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuotesPackItems_QuotesTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "QuotesTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuotesPackLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotesPackLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuotesPackLogs_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuotesPackLogs_QuotesPacks_PackId",
                        column: x => x.PackId,
                        principalTable: "QuotesPacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuotesPackItems_PackId",
                table: "QuotesPackItems",
                column: "PackId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotesPackItems_TypeId",
                table: "QuotesPackItems",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotesPackLogs_CustomerId",
                table: "QuotesPackLogs",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotesPackLogs_PackId",
                table: "QuotesPackLogs",
                column: "PackId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerQuotes_QuotesTypes_TypeId",
                table: "CustomerQuotes",
                column: "TypeId",
                principalTable: "QuotesTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotesLogs_QuotesTypes_TypeId",
                table: "QuotesLogs",
                column: "TypeId",
                principalTable: "QuotesTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerQuotes_QuotesTypes_TypeId",
                table: "CustomerQuotes");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotesLogs_QuotesTypes_TypeId",
                table: "QuotesLogs");

            migrationBuilder.DropTable(
                name: "QuotesPackItems");

            migrationBuilder.DropTable(
                name: "QuotesPackLogs");

            migrationBuilder.DropTable(
                name: "QuotesPacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuotesTypes",
                table: "QuotesTypes");

            migrationBuilder.RenameTable(
                name: "QuotesTypes",
                newName: "QuotesType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuotesType",
                table: "QuotesType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerQuotes_QuotesType_TypeId",
                table: "CustomerQuotes",
                column: "TypeId",
                principalTable: "QuotesType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotesLogs_QuotesType_TypeId",
                table: "QuotesLogs",
                column: "TypeId",
                principalTable: "QuotesType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

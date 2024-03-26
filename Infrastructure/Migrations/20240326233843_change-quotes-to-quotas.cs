using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changequotestoquotas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerQuotes");

            migrationBuilder.DropTable(
                name: "QuotesLogs");

            migrationBuilder.DropTable(
                name: "QuotesPackItems");

            migrationBuilder.DropTable(
                name: "QuotesPackLogs");

            migrationBuilder.DropTable(
                name: "QuotesTypes");

            migrationBuilder.DropTable(
                name: "QuotesPacks");

            migrationBuilder.CreateTable(
                name: "QuotasPacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotasPacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuotasTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotasTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuotasPackLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotasPackLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuotasPackLogs_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuotasPackLogs_QuotasPacks_PackId",
                        column: x => x.PackId,
                        principalTable: "QuotasPacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerQuotas",
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
                    table.PrimaryKey("PK_CustomerQuotas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerQuotas_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerQuotas_QuotasTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "QuotasTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuotasLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotasLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuotasLogs_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuotasLogs_QuotasTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "QuotasTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuotasPackItems",
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
                    table.PrimaryKey("PK_QuotasPackItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuotasPackItems_QuotasPacks_PackId",
                        column: x => x.PackId,
                        principalTable: "QuotasPacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuotasPackItems_QuotasTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "QuotasTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "QuotasTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { "Clustering", "Clusterization of data", "Clustering" },
                    { "Embeddings", "Creating embeddings", "Embeddings" },
                    { "PrivateProfiles", "Creating private profiles", "Private profiles" },
                    { "PrivateWorkspaces", "Creating private workspaces", "Private workspaces" },
                    { "PublicProfiles", "Creating public profiles", "Public profiles" },
                    { "PublicWorkspaces", "Creating public workspaces", "Public workspaces" },
                    { "Youtube", "Loading data from Youtube", "Youtube" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerQuotas_CustomerId",
                table: "CustomerQuotas",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerQuotas_TypeId",
                table: "CustomerQuotas",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotasLogs_CustomerId",
                table: "QuotasLogs",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotasLogs_TypeId",
                table: "QuotasLogs",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotasPackItems_PackId",
                table: "QuotasPackItems",
                column: "PackId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotasPackItems_TypeId",
                table: "QuotasPackItems",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotasPackLogs_CustomerId",
                table: "QuotasPackLogs",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotasPackLogs_PackId",
                table: "QuotasPackLogs",
                column: "PackId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerQuotas");

            migrationBuilder.DropTable(
                name: "QuotasLogs");

            migrationBuilder.DropTable(
                name: "QuotasPackItems");

            migrationBuilder.DropTable(
                name: "QuotasPackLogs");

            migrationBuilder.DropTable(
                name: "QuotasTypes");

            migrationBuilder.DropTable(
                name: "QuotasPacks");

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
                name: "QuotesTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotesTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuotesPackLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PackId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "CustomerQuotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AvailableCount = table.Column<int>(type: "int", nullable: false),
                    ExpiredCount = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_CustomerQuotes_QuotesTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "QuotesTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuotesLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TypeId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                        name: "FK_QuotesLogs_QuotesTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "QuotesTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuotesPackItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackId = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.InsertData(
                table: "QuotesTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { "Clustering", "Clusterization of data", "Clustering" },
                    { "Embeddings", "Creating embeddings", "Embeddings" },
                    { "PrivateProfiles", "Creating private profiles", "Private profiles" },
                    { "PrivateWorkspaces", "Creating private workspaces", "Private workspaces" },
                    { "PublicProfiles", "Creating public profiles", "Public profiles" },
                    { "PublicWorkspaces", "Creating public workspaces", "Public workspaces" },
                    { "Youtube", "Loading data from Youtube", "Youtube" }
                });

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
        }
    }
}

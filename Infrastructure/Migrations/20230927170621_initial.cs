using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Channels",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ETag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishedAtDateTimeOffset = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PublishedAtRaw = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubscriberCount = table.Column<long>(type: "bigint", nullable: false),
                    VideoCount = table.Column<int>(type: "int", nullable: false),
                    ViewCount = table.Column<long>(type: "bigint", nullable: false),
                    LoadedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChannelImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClusterizationAlgorithms",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterizationAlgorithms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClusterizationDimensionTypes",
                columns: table => new
                {
                    DimensionCount = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterizationDimensionTypes", x => x.DimensionCount);
                });

            migrationBuilder.CreateTable(
                name: "ClusterizationTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterizationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MyTaskStates",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyTaskStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ETag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChannelTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LiveBroadcaseContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishedAtDateTimeOffset = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PublishedAtRaw = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultAudioLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentCount = table.Column<int>(type: "int", nullable: false),
                    LikeCount = table.Column<int>(type: "int", nullable: false),
                    ViewCount = table.Column<long>(type: "bigint", nullable: false),
                    ChannelId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoadedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videos_Channels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClusterizationWorkspaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsAllDataEmbedded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterizationWorkspaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClusterizationWorkspaces_ClusterizationTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ClusterizationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MyTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Percent = table.Column<float>(type: "real", nullable: false),
                    StateId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyTasks_MyTaskStates_StateId",
                        column: x => x.StateId,
                        principalTable: "MyTaskStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ETag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CanReply = table.Column<bool>(type: "bit", nullable: false),
                    TotalReplyCount = table.Column<short>(type: "smallint", nullable: false),
                    AuthorChannelUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorProfileImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorChannelId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LikeCount = table.Column<int>(type: "int", nullable: false),
                    TextDisplay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextOriginal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAtDateTimeOffset = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAtRaw = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChannelId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VideoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishedAtDateTimeOffset = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PublishedAtRaw = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoadedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmbeddingDataId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Channels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClusterizationProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlgorithmId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DimensionTypeId = table.Column<int>(type: "int", nullable: false),
                    WorkspaceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterizationProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClusterizationProfiles_ClusterizationAlgorithms_AlgorithmId",
                        column: x => x.AlgorithmId,
                        principalTable: "ClusterizationAlgorithms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClusterizationProfiles_ClusterizationDimensionTypes_DimensionTypeId",
                        column: x => x.DimensionTypeId,
                        principalTable: "ClusterizationDimensionTypes",
                        principalColumn: "DimensionCount",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClusterizationProfiles_ClusterizationWorkspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalTable: "ClusterizationWorkspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClusterizationWorkspaceComment",
                columns: table => new
                {
                    CommentsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkspacesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterizationWorkspaceComment", x => new { x.CommentsId, x.WorkspacesId });
                    table.ForeignKey(
                        name: "FK_ClusterizationWorkspaceComment_ClusterizationWorkspaces_WorkspacesId",
                        column: x => x.WorkspacesId,
                        principalTable: "ClusterizationWorkspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClusterizationWorkspaceComment_Comments_CommentsId",
                        column: x => x.CommentsId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmbeddingDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmbeddingDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmbeddingDatas_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClusterizationTiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X = table.Column<int>(type: "int", nullable: false),
                    Y = table.Column<int>(type: "int", nullable: false),
                    Z = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    ProfileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterizationTiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClusterizationTiles_ClusterizationProfiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "ClusterizationProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClusterizationTiles_ClusterizationTiles_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ClusterizationTiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmbeddingDimensionValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DimensionTypeId = table.Column<int>(type: "int", nullable: false),
                    EmbeddingDataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmbeddingDimensionValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmbeddingDimensionValues_ClusterizationDimensionTypes_DimensionTypeId",
                        column: x => x.DimensionTypeId,
                        principalTable: "ClusterizationDimensionTypes",
                        principalColumn: "DimensionCount",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmbeddingDimensionValues_EmbeddingDatas_EmbeddingDataId",
                        column: x => x.EmbeddingDataId,
                        principalTable: "EmbeddingDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DisplayedPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X = table.Column<double>(type: "float", nullable: false),
                    Y = table.Column<double>(type: "float", nullable: false),
                    OptimizationLevel = table.Column<int>(type: "int", nullable: false),
                    ValueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TileId = table.Column<int>(type: "int", nullable: false),
                    ParentPointId = table.Column<int>(type: "int", nullable: true),
                    ClusterizationEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisplayedPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisplayedPoints_ClusterizationTiles_TileId",
                        column: x => x.TileId,
                        principalTable: "ClusterizationTiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisplayedPoints_DisplayedPoints_ParentPointId",
                        column: x => x.ParentPointId,
                        principalTable: "DisplayedPoints",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmbeddingValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<double>(type: "float", nullable: false),
                    EmbeddingDimensionValueId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "ClusterizationEntites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmbeddingDataId = table.Column<int>(type: "int", nullable: true),
                    CommentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisplayedPointId = table.Column<int>(type: "int", nullable: true),
                    WorkspaceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterizationEntites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClusterizationEntites_ClusterizationWorkspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalTable: "ClusterizationWorkspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClusterizationEntites_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClusterizationEntites_DisplayedPoints_DisplayedPointId",
                        column: x => x.DisplayedPointId,
                        principalTable: "DisplayedPoints",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClusterizationEntites_EmbeddingDatas_EmbeddingDataId",
                        column: x => x.EmbeddingDataId,
                        principalTable: "EmbeddingDatas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClusterizationPointColors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PointId = table.Column<int>(type: "int", nullable: false),
                    ProfileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterizationPointColors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClusterizationPointColors_ClusterizationProfiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "ClusterizationProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClusterizationPointColors_DisplayedPoints_PointId",
                        column: x => x.PointId,
                        principalTable: "DisplayedPoints",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClusterizationColorValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PointColorsId = table.Column<int>(type: "int", nullable: true),
                    ClusterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterizationColorValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClusterizationColorValues_ClusterizationPointColors_PointColorsId",
                        column: x => x.PointColorsId,
                        principalTable: "ClusterizationPointColors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Clusters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    ProfileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clusters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clusters_ClusterizationColorValues_ColorId",
                        column: x => x.ColorId,
                        principalTable: "ClusterizationColorValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Clusters_ClusterizationProfiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "ClusterizationProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClusterClusterizationEntity",
                columns: table => new
                {
                    ClustersId = table.Column<int>(type: "int", nullable: false),
                    EntitiesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterClusterizationEntity", x => new { x.ClustersId, x.EntitiesId });
                    table.ForeignKey(
                        name: "FK_ClusterClusterizationEntity_ClusterizationEntites_EntitiesId",
                        column: x => x.EntitiesId,
                        principalTable: "ClusterizationEntites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClusterClusterizationEntity_Clusters_ClustersId",
                        column: x => x.ClustersId,
                        principalTable: "Clusters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "ClusterizationDimensionTypes",
                column: "DimensionCount",
                values: new object[]
                {
                    2,
                    3,
                    100,
                    1536
                });

            migrationBuilder.InsertData(
                table: "ClusterizationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "Channels", "Канали" },
                    { "Comments", "Коментарі" },
                    { "Videos", "Відео" }
                });

            migrationBuilder.InsertData(
                table: "MyTaskStates",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "Completed", "Виконалася" },
                    { "Error", "Помилка" },
                    { "Process", "Виконується" },
                    { "Stopped", "Призупинено" },
                    { "Wait", "Очікування" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClusterClusterizationEntity_EntitiesId",
                table: "ClusterClusterizationEntity",
                column: "EntitiesId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationColorValues_PointColorsId",
                table: "ClusterizationColorValues",
                column: "PointColorsId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationEntites_CommentId",
                table: "ClusterizationEntites",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationEntites_DisplayedPointId",
                table: "ClusterizationEntites",
                column: "DisplayedPointId",
                unique: true,
                filter: "[DisplayedPointId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationEntites_EmbeddingDataId",
                table: "ClusterizationEntites",
                column: "EmbeddingDataId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationEntites_WorkspaceId",
                table: "ClusterizationEntites",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationPointColors_PointId",
                table: "ClusterizationPointColors",
                column: "PointId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationPointColors_ProfileId",
                table: "ClusterizationPointColors",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationProfiles_AlgorithmId",
                table: "ClusterizationProfiles",
                column: "AlgorithmId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationProfiles_DimensionTypeId",
                table: "ClusterizationProfiles",
                column: "DimensionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationProfiles_WorkspaceId",
                table: "ClusterizationProfiles",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationTiles_ParentId",
                table: "ClusterizationTiles",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationTiles_ProfileId",
                table: "ClusterizationTiles",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationWorkspaceComment_WorkspacesId",
                table: "ClusterizationWorkspaceComment",
                column: "WorkspacesId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationWorkspaces_TypeId",
                table: "ClusterizationWorkspaces",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Clusters_ColorId",
                table: "Clusters",
                column: "ColorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clusters_ProfileId",
                table: "Clusters",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ChannelId",
                table: "Comments",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_VideoId",
                table: "Comments",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_DisplayedPoints_ParentPointId",
                table: "DisplayedPoints",
                column: "ParentPointId");

            migrationBuilder.CreateIndex(
                name: "IX_DisplayedPoints_TileId",
                table: "DisplayedPoints",
                column: "TileId");

            migrationBuilder.CreateIndex(
                name: "IX_EmbeddingDatas_CommentId",
                table: "EmbeddingDatas",
                column: "CommentId",
                unique: true,
                filter: "[CommentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EmbeddingDimensionValues_DimensionTypeId",
                table: "EmbeddingDimensionValues",
                column: "DimensionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmbeddingDimensionValues_EmbeddingDataId",
                table: "EmbeddingDimensionValues",
                column: "EmbeddingDataId");

            migrationBuilder.CreateIndex(
                name: "IX_EmbeddingValues_EmbeddingDimensionValueId",
                table: "EmbeddingValues",
                column: "EmbeddingDimensionValueId");

            migrationBuilder.CreateIndex(
                name: "IX_MyTasks_StateId",
                table: "MyTasks",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_ChannelId",
                table: "Videos",
                column: "ChannelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClusterClusterizationEntity");

            migrationBuilder.DropTable(
                name: "ClusterizationWorkspaceComment");

            migrationBuilder.DropTable(
                name: "EmbeddingValues");

            migrationBuilder.DropTable(
                name: "MyTasks");

            migrationBuilder.DropTable(
                name: "ClusterizationEntites");

            migrationBuilder.DropTable(
                name: "Clusters");

            migrationBuilder.DropTable(
                name: "EmbeddingDimensionValues");

            migrationBuilder.DropTable(
                name: "MyTaskStates");

            migrationBuilder.DropTable(
                name: "ClusterizationColorValues");

            migrationBuilder.DropTable(
                name: "EmbeddingDatas");

            migrationBuilder.DropTable(
                name: "ClusterizationPointColors");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "DisplayedPoints");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "ClusterizationTiles");

            migrationBuilder.DropTable(
                name: "Channels");

            migrationBuilder.DropTable(
                name: "ClusterizationProfiles");

            migrationBuilder.DropTable(
                name: "ClusterizationAlgorithms");

            migrationBuilder.DropTable(
                name: "ClusterizationDimensionTypes");

            migrationBuilder.DropTable(
                name: "ClusterizationWorkspaces");

            migrationBuilder.DropTable(
                name: "ClusterizationTypes");
        }
    }
}

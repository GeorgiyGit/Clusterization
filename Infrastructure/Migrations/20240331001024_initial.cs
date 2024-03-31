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
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastDeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsEdited = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClusterizationAlgorithmType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterizationAlgorithmType", x => x.Id);
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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    LoadedVideoCount = table.Column<int>(type: "int", nullable: false),
                    LoadedCommentCount = table.Column<int>(type: "int", nullable: false),
                    ViewCount = table.Column<long>(type: "bigint", nullable: false),
                    LoadedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChannelImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoaderId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Channels_AspNetUsers_LoaderId",
                        column: x => x.LoaderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClusterizationAbstractAlgorithms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    Epsilon = table.Column<double>(type: "float", nullable: true),
                    MinimumPointsPerCluster = table.Column<int>(type: "int", nullable: true),
                    NumberOfComponents = table.Column<int>(type: "int", nullable: true),
                    NumClusters = table.Column<int>(type: "int", nullable: true),
                    Seed = table.Column<int>(type: "int", nullable: true),
                    ClusterColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpectralClusteringAlgorithm_NumClusters = table.Column<int>(type: "int", nullable: true),
                    Gamma = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterizationAbstractAlgorithms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClusterizationAbstractAlgorithms_ClusterizationAlgorithmType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ClusterizationAlgorithmType",
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
                    IsAllDataEmbedded = table.Column<bool>(type: "bit", nullable: false),
                    EntitiesCount = table.Column<int>(type: "int", nullable: false),
                    VisibleType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChangingType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterizationWorkspaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClusterizationWorkspaces_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    StateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyTasks_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MyTasks_MyTaskStates_StateId",
                        column: x => x.StateId,
                        principalTable: "MyTaskStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuotasPackLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastDeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsEdited = table.Column<bool>(type: "bit", nullable: false)
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
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastDeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsEdited = table.Column<bool>(type: "bit", nullable: false)
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
                    LoadedCommentCount = table.Column<int>(type: "int", nullable: false),
                    LikeCount = table.Column<int>(type: "int", nullable: false),
                    ViewCount = table.Column<long>(type: "bigint", nullable: false),
                    ChannelId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoadedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoaderId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videos_AspNetUsers_LoaderId",
                        column: x => x.LoaderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Videos_Channels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ClusterizationProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlgorithmId = table.Column<int>(type: "int", nullable: false),
                    DimensionCount = table.Column<int>(type: "int", nullable: false),
                    DimensionalityReductionTechniqueId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkspaceId = table.Column<int>(type: "int", nullable: false),
                    IsCalculated = table.Column<bool>(type: "bit", nullable: false),
                    MinTileLevel = table.Column<int>(type: "int", nullable: false),
                    MaxTileLevel = table.Column<int>(type: "int", nullable: false),
                    IsElected = table.Column<bool>(type: "bit", nullable: false),
                    VisibleType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChangingType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterizationProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClusterizationProfiles_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClusterizationProfiles_ClusterizationAbstractAlgorithms_AlgorithmId",
                        column: x => x.AlgorithmId,
                        principalTable: "ClusterizationAbstractAlgorithms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClusterizationProfiles_ClusterizationDimensionTypes_DimensionCount",
                        column: x => x.DimensionCount,
                        principalTable: "ClusterizationDimensionTypes",
                        principalColumn: "DimensionCount",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClusterizationProfiles_ClusterizationWorkspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalTable: "ClusterizationWorkspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ClusterizationProfiles_DimensionalityReductionTechniques_DimensionalityReductionTechniqueId",
                        column: x => x.DimensionalityReductionTechniqueId,
                        principalTable: "DimensionalityReductionTechniques",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClusterizationWorkspaceDRTechniques",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DRTechniqueId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkspaceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterizationWorkspaceDRTechniques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClusterizationWorkspaceDRTechniques_ClusterizationWorkspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalTable: "ClusterizationWorkspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClusterizationWorkspaceDRTechniques_DimensionalityReductionTechniques_DRTechniqueId",
                        column: x => x.DRTechniqueId,
                        principalTable: "DimensionalityReductionTechniques",
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
                    EmbeddingDataId = table.Column<int>(type: "int", nullable: true),
                    LoaderId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_LoaderId",
                        column: x => x.LoaderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Channels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Comments_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClusterizationTilesLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Z = table.Column<int>(type: "int", nullable: false),
                    TileLength = table.Column<double>(type: "float", nullable: false),
                    TileCount = table.Column<int>(type: "int", nullable: false),
                    MinXValue = table.Column<double>(type: "float", nullable: false),
                    MinYValue = table.Column<double>(type: "float", nullable: false),
                    MaxXValue = table.Column<double>(type: "float", nullable: false),
                    MaxYValue = table.Column<double>(type: "float", nullable: false),
                    ProfileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterizationTilesLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClusterizationTilesLevels_ClusterizationProfiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "ClusterizationProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clusters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileId = table.Column<int>(type: "int", nullable: false),
                    ParentClusterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clusters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clusters_ClusterizationProfiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "ClusterizationProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clusters_Clusters_ParentClusterId",
                        column: x => x.ParentClusterId,
                        principalTable: "Clusters",
                        principalColumn: "Id");
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
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ClusterizationTiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Length = table.Column<double>(type: "float", nullable: false),
                    X = table.Column<int>(type: "int", nullable: false),
                    Y = table.Column<int>(type: "int", nullable: false),
                    Z = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    ProfileId = table.Column<int>(type: "int", nullable: false),
                    TilesLevelId = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_ClusterizationTiles_ClusterizationTilesLevels_TilesLevelId",
                        column: x => x.TilesLevelId,
                        principalTable: "ClusterizationTilesLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ClusterizationTiles_ClusterizationTiles_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ClusterizationTiles",
                        principalColumn: "Id");
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
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TileId = table.Column<int>(type: "int", nullable: false),
                    ClusterId = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_DisplayedPoints_Clusters_ClusterId",
                        column: x => x.ClusterId,
                        principalTable: "Clusters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
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
                        name: "FK_ClusterClusterizationEntity_Clusters_ClustersId",
                        column: x => x.ClustersId,
                        principalTable: "Clusters",
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
                    CommentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ExternalObjectId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    WorkspaceId = table.Column<int>(type: "int", nullable: false),
                    TextValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DimensionalityReductionValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmbeddingDataId = table.Column<int>(type: "int", nullable: true),
                    ClusterizationEntityId = table.Column<int>(type: "int", nullable: true),
                    ClusterizationWorkspaceDRTechniqueId = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_DimensionalityReductionValues_ClusterizationWorkspaceDRTechniques_ClusterizationWorkspaceDRTechniqueId",
                        column: x => x.ClusterizationWorkspaceDRTechniqueId,
                        principalTable: "ClusterizationWorkspaceDRTechniques",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DimensionalityReductionValues_DimensionalityReductionTechniques_TechniqueId",
                        column: x => x.TechniqueId,
                        principalTable: "DimensionalityReductionTechniques",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmbeddingDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginalEmbeddingId = table.Column<int>(type: "int", nullable: false),
                    DimensionalityReductionValueId = table.Column<int>(type: "int", nullable: false),
                    CommentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ExternalObjectId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmbeddingDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmbeddingDatas_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmbeddingDatas_DimensionalityReductionValues_DimensionalityReductionValueId",
                        column: x => x.DimensionalityReductionValueId,
                        principalTable: "DimensionalityReductionValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmbeddingDimensionValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DimensionTypeId = table.Column<int>(type: "int", nullable: false),
                    EmbeddingDataId = table.Column<int>(type: "int", nullable: true),
                    DimensionalityReductionValueId = table.Column<int>(type: "int", nullable: true),
                    ValuesString = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                        name: "FK_EmbeddingDimensionValues_DimensionalityReductionValues_DimensionalityReductionValueId",
                        column: x => x.DimensionalityReductionValueId,
                        principalTable: "DimensionalityReductionValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmbeddingDimensionValues_EmbeddingDatas_EmbeddingDataId",
                        column: x => x.EmbeddingDataId,
                        principalTable: "EmbeddingDatas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExternalObjects",
                columns: table => new
                {
                    FullId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Session = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmbeddingDataId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalObjects", x => x.FullId);
                    table.ForeignKey(
                        name: "FK_ExternalObjects_EmbeddingDatas_EmbeddingDataId",
                        column: x => x.EmbeddingDataId,
                        principalTable: "EmbeddingDatas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClusterizationWorkspaceExternalObject",
                columns: table => new
                {
                    ExternalObjectsFullId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkspacesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterizationWorkspaceExternalObject", x => new { x.ExternalObjectsFullId, x.WorkspacesId });
                    table.ForeignKey(
                        name: "FK_ClusterizationWorkspaceExternalObject_ClusterizationWorkspaces_WorkspacesId",
                        column: x => x.WorkspacesId,
                        principalTable: "ClusterizationWorkspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClusterizationWorkspaceExternalObject_ExternalObjects_ExternalObjectsFullId",
                        column: x => x.ExternalObjectsFullId,
                        principalTable: "ExternalObjects",
                        principalColumn: "FullId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ClusterizationAlgorithmType",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { "DBScan", "Density-Based Spatial Clustering Of Applications With Noise", "DBSCAN" },
                    { "GaussianMixture", "A clustering method that models the data as a mixture of Gaussian partitions", "Gaussian Mixture" },
                    { "KMeans", "Arrangement of a set of objects into relatively homogeneous groups.", "k-means" },
                    { "OneCluster", "Combining all elements into one cluster", "One cluster" },
                    { "SpectralClustering", "Spectral clustering is based on the principles of graph theory and linear algebra", "Spectral Clustering" }
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
                    { "Comments", "Comments" },
                    { "External", "From file" }
                });

            migrationBuilder.InsertData(
                table: "DimensionalityReductionTechniques",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "JSL", "Johnson-Lindenstrauss lemma" },
                    { "PCA", "Principal Component Analysis" },
                    { "t-SNE", "t-Distributed Stochastic Neighbor Embedding" }
                });

            migrationBuilder.InsertData(
                table: "MyTaskStates",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "Completed", "Completed" },
                    { "Error", "Error" },
                    { "Process", "Process" },
                    { "Stopped", "Stopped" },
                    { "Wait", "Wait" }
                });

            migrationBuilder.InsertData(
                table: "QuotasPacks",
                column: "Id",
                values: new object[]
                {
                    1,
                    2
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

            migrationBuilder.InsertData(
                table: "QuotasPackItems",
                columns: new[] { "Id", "Count", "PackId", "TypeId" },
                values: new object[,]
                {
                    { 1, 1000, 1, "Youtube" },
                    { 2, 1000, 1, "Embeddings" },
                    { 3, 10000, 1, "Clustering" },
                    { 4, 5, 1, "PublicWorkspaces" },
                    { 5, 20, 1, "PrivateWorkspaces" },
                    { 6, 20, 1, "PublicProfiles" },
                    { 7, 50, 1, "PrivateProfiles" },
                    { 8, 1000000000, 2, "Youtube" },
                    { 9, 1000000000, 2, "Embeddings" },
                    { 10, 1000000000, 2, "Clustering" },
                    { 11, 1000000000, 2, "PublicWorkspaces" },
                    { 12, 1000000000, 2, "PrivateWorkspaces" },
                    { 13, 1000000000, 2, "PublicProfiles" },
                    { 14, 1000000000, 2, "PrivateProfiles" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterClusterizationEntity_EntitiesId",
                table: "ClusterClusterizationEntity",
                column: "EntitiesId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationAbstractAlgorithms_TypeId",
                table: "ClusterizationAbstractAlgorithms",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationEntites_CommentId",
                table: "ClusterizationEntites",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationEntites_EmbeddingDataId",
                table: "ClusterizationEntites",
                column: "EmbeddingDataId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationEntites_ExternalObjectId",
                table: "ClusterizationEntites",
                column: "ExternalObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationEntites_WorkspaceId",
                table: "ClusterizationEntites",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationProfiles_AlgorithmId",
                table: "ClusterizationProfiles",
                column: "AlgorithmId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationProfiles_DimensionalityReductionTechniqueId",
                table: "ClusterizationProfiles",
                column: "DimensionalityReductionTechniqueId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationProfiles_DimensionCount",
                table: "ClusterizationProfiles",
                column: "DimensionCount");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationProfiles_OwnerId",
                table: "ClusterizationProfiles",
                column: "OwnerId");

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
                name: "IX_ClusterizationTiles_TilesLevelId",
                table: "ClusterizationTiles",
                column: "TilesLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationTilesLevels_ProfileId",
                table: "ClusterizationTilesLevels",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationWorkspaceComment_WorkspacesId",
                table: "ClusterizationWorkspaceComment",
                column: "WorkspacesId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationWorkspaceDRTechniques_DRTechniqueId",
                table: "ClusterizationWorkspaceDRTechniques",
                column: "DRTechniqueId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationWorkspaceDRTechniques_WorkspaceId",
                table: "ClusterizationWorkspaceDRTechniques",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationWorkspaceExternalObject_WorkspacesId",
                table: "ClusterizationWorkspaceExternalObject",
                column: "WorkspacesId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationWorkspaces_OwnerId",
                table: "ClusterizationWorkspaces",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationWorkspaces_TypeId",
                table: "ClusterizationWorkspaces",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Clusters_ParentClusterId",
                table: "Clusters",
                column: "ParentClusterId");

            migrationBuilder.CreateIndex(
                name: "IX_Clusters_ProfileId",
                table: "Clusters",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ChannelId",
                table: "Comments",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_LoaderId",
                table: "Comments",
                column: "LoaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PublishedAtDateTimeOffset",
                table: "Comments",
                column: "PublishedAtDateTimeOffset");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_VideoId",
                table: "Comments",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerQuotas_CustomerId",
                table: "CustomerQuotas",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerQuotas_TypeId",
                table: "CustomerQuotas",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DimensionalityReductionValues_ClusterizationEntityId",
                table: "DimensionalityReductionValues",
                column: "ClusterizationEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_DimensionalityReductionValues_ClusterizationWorkspaceDRTechniqueId",
                table: "DimensionalityReductionValues",
                column: "ClusterizationWorkspaceDRTechniqueId");

            migrationBuilder.CreateIndex(
                name: "IX_DimensionalityReductionValues_TechniqueId_ClusterizationWorkspaceDRTechniqueId",
                table: "DimensionalityReductionValues",
                columns: new[] { "TechniqueId", "ClusterizationWorkspaceDRTechniqueId" });

            migrationBuilder.CreateIndex(
                name: "IX_DisplayedPoints_ClusterId",
                table: "DisplayedPoints",
                column: "ClusterId");

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
                name: "IX_EmbeddingDatas_DimensionalityReductionValueId",
                table: "EmbeddingDatas",
                column: "DimensionalityReductionValueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmbeddingDimensionValues_DimensionalityReductionValueId",
                table: "EmbeddingDimensionValues",
                column: "DimensionalityReductionValueId");

            migrationBuilder.CreateIndex(
                name: "IX_EmbeddingDimensionValues_DimensionTypeId",
                table: "EmbeddingDimensionValues",
                column: "DimensionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmbeddingDimensionValues_EmbeddingDataId",
                table: "EmbeddingDimensionValues",
                column: "EmbeddingDataId",
                unique: true,
                filter: "[EmbeddingDataId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalObjects_EmbeddingDataId",
                table: "ExternalObjects",
                column: "EmbeddingDataId",
                unique: true,
                filter: "[EmbeddingDataId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Channels_LoaderId",
                table: "Channels",
                column: "LoaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Channels_PublishedAtDateTimeOffset_VideoCount_SubscriberCount",
                table: "Channels",
                columns: new[] { "PublishedAtDateTimeOffset", "VideoCount", "SubscriberCount" });

            migrationBuilder.CreateIndex(
                name: "IX_MyTasks_CustomerId",
                table: "MyTasks",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_MyTasks_StateId",
                table: "MyTasks",
                column: "StateId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Videos_ChannelId",
                table: "Videos",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_LoaderId",
                table: "Videos",
                column: "LoaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_PublishedAtDateTimeOffset_CommentCount_ViewCount",
                table: "Videos",
                columns: new[] { "PublishedAtDateTimeOffset", "CommentCount", "ViewCount" });

            migrationBuilder.AddForeignKey(
                name: "FK_ClusterClusterizationEntity_ClusterizationEntites_EntitiesId",
                table: "ClusterClusterizationEntity",
                column: "EntitiesId",
                principalTable: "ClusterizationEntites",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ClusterizationEntites_EmbeddingDatas_EmbeddingDataId",
                table: "ClusterizationEntites",
                column: "EmbeddingDataId",
                principalTable: "EmbeddingDatas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClusterizationEntites_ExternalObjects_ExternalObjectId",
                table: "ClusterizationEntites",
                column: "ExternalObjectId",
                principalTable: "ExternalObjects",
                principalColumn: "FullId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClusterizationWorkspaces_AspNetUsers_OwnerId",
                table: "ClusterizationWorkspaces");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_LoaderId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Channels_AspNetUsers_LoaderId",
                table: "Channels");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_AspNetUsers_LoaderId",
                table: "Videos");

            migrationBuilder.DropForeignKey(
                name: "FK_DimensionalityReductionValues_ClusterizationEntites_ClusterizationEntityId",
                table: "DimensionalityReductionValues");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ClusterClusterizationEntity");

            migrationBuilder.DropTable(
                name: "ClusterizationWorkspaceComment");

            migrationBuilder.DropTable(
                name: "ClusterizationWorkspaceExternalObject");

            migrationBuilder.DropTable(
                name: "CustomerQuotas");

            migrationBuilder.DropTable(
                name: "DisplayedPoints");

            migrationBuilder.DropTable(
                name: "EmbeddingDimensionValues");

            migrationBuilder.DropTable(
                name: "MyTasks");

            migrationBuilder.DropTable(
                name: "QuotasLogs");

            migrationBuilder.DropTable(
                name: "QuotasPackItems");

            migrationBuilder.DropTable(
                name: "QuotasPackLogs");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ClusterizationTiles");

            migrationBuilder.DropTable(
                name: "Clusters");

            migrationBuilder.DropTable(
                name: "MyTaskStates");

            migrationBuilder.DropTable(
                name: "QuotasTypes");

            migrationBuilder.DropTable(
                name: "QuotasPacks");

            migrationBuilder.DropTable(
                name: "ClusterizationTilesLevels");

            migrationBuilder.DropTable(
                name: "ClusterizationProfiles");

            migrationBuilder.DropTable(
                name: "ClusterizationAbstractAlgorithms");

            migrationBuilder.DropTable(
                name: "ClusterizationDimensionTypes");

            migrationBuilder.DropTable(
                name: "ClusterizationAlgorithmType");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ClusterizationEntites");

            migrationBuilder.DropTable(
                name: "ExternalObjects");

            migrationBuilder.DropTable(
                name: "EmbeddingDatas");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "DimensionalityReductionValues");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "ClusterizationWorkspaceDRTechniques");

            migrationBuilder.DropTable(
                name: "Channels");

            migrationBuilder.DropTable(
                name: "ClusterizationWorkspaces");

            migrationBuilder.DropTable(
                name: "DimensionalityReductionTechniques");

            migrationBuilder.DropTable(
                name: "ClusterizationTypes");
        }
    }
}

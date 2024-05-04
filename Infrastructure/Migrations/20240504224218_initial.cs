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
                name: "ClusterizationAlgorithmTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterizationAlgorithmTypes", x => x.Id);
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
                name: "DimensionTypes",
                columns: table => new
                {
                    DimensionCount = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimensionTypes", x => x.DimensionCount);
                });

            migrationBuilder.CreateTable(
                name: "MyDataObjectType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyDataObjectType", x => x.Id);
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
                name: "TelegramChannels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TelegramID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoId = table.Column<long>(type: "bigint", nullable: false),
                    ParticipantsCount = table.Column<long>(type: "bigint", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelegramMessagesCount = table.Column<int>(type: "int", nullable: false),
                    TelegramRepliesCount = table.Column<int>(type: "int", nullable: false),
                    LoadedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoaderId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramChannels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TelegramChannels_AspNetUsers_LoaderId",
                        column: x => x.LoaderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "YoutubeChannels",
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
                    table.PrimaryKey("PK_YoutubeChannels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YoutubeChannels_AspNetUsers_LoaderId",
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
                        name: "FK_ClusterizationAbstractAlgorithms_ClusterizationAlgorithmTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ClusterizationAlgorithmTypes",
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
                    TypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EntitiesCount = table.Column<int>(type: "int", nullable: false),
                    VisibleType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChangingType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsProfilesInCalculation = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastDeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsEdited = table.Column<bool>(type: "bit", nullable: false)
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
                name: "EmbeddingModels",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxInputCount = table.Column<int>(type: "int", nullable: false),
                    QuotasPrice = table.Column<int>(type: "int", nullable: false),
                    DimensionTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmbeddingModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmbeddingModels_DimensionTypes_DimensionTypeId",
                        column: x => x.DimensionTypeId,
                        principalTable: "DimensionTypes",
                        principalColumn: "DimensionCount",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MyDataObject",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    YoutubeCommentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExternalObjectId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelegramMessageId = table.Column<long>(type: "bigint", nullable: true),
                    TelegramReplyId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyDataObject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyDataObject_MyDataObjectType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "MyDataObjectType",
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
                name: "YoutubeVideos",
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
                    table.PrimaryKey("PK_YoutubeVideos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YoutubeVideos_AspNetUsers_LoaderId",
                        column: x => x.LoaderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_YoutubeVideos_YoutubeChannels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "YoutubeChannels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkspaceDataObjectsAddPacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataObjectsCount = table.Column<int>(type: "int", nullable: false),
                    WorkspaceId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastDeleteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsEdited = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkspaceDataObjectsAddPacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkspaceDataObjectsAddPacks_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkspaceDataObjectsAddPacks_ClusterizationWorkspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalTable: "ClusterizationWorkspaces",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClusterizationProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlgorithmId = table.Column<int>(type: "int", nullable: false),
                    DimensionCount = table.Column<int>(type: "int", nullable: false),
                    DRTechniqueId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkspaceId = table.Column<int>(type: "int", nullable: false),
                    IsInCalculation = table.Column<bool>(type: "bit", nullable: false),
                    IsCalculated = table.Column<bool>(type: "bit", nullable: false),
                    MinTileLevel = table.Column<int>(type: "int", nullable: false),
                    MaxTileLevel = table.Column<int>(type: "int", nullable: false),
                    IsElected = table.Column<bool>(type: "bit", nullable: false),
                    VisibleType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChangingType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmbeddingModelId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmbeddingLoadingStateId = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_ClusterizationProfiles_ClusterizationWorkspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalTable: "ClusterizationWorkspaces",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClusterizationProfiles_DimensionTypes_DimensionCount",
                        column: x => x.DimensionCount,
                        principalTable: "DimensionTypes",
                        principalColumn: "DimensionCount",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClusterizationProfiles_DimensionalityReductionTechniques_DRTechniqueId",
                        column: x => x.DRTechniqueId,
                        principalTable: "DimensionalityReductionTechniques",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClusterizationProfiles_EmbeddingModels_EmbeddingModelId",
                        column: x => x.EmbeddingModelId,
                        principalTable: "EmbeddingModels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClusterizationWorkspaceMyDataObject",
                columns: table => new
                {
                    DataObjectsId = table.Column<long>(type: "bigint", nullable: false),
                    WorkspacesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterizationWorkspaceMyDataObject", x => new { x.DataObjectsId, x.WorkspacesId });
                    table.ForeignKey(
                        name: "FK_ClusterizationWorkspaceMyDataObject_ClusterizationWorkspaces_WorkspacesId",
                        column: x => x.WorkspacesId,
                        principalTable: "ClusterizationWorkspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClusterizationWorkspaceMyDataObject_MyDataObject_DataObjectsId",
                        column: x => x.DataObjectsId,
                        principalTable: "MyDataObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmbeddingObjectsGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataObjectId = table.Column<long>(type: "bigint", nullable: false),
                    DRTechniqueId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmbeddingModelId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkspaceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmbeddingObjectsGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmbeddingObjectsGroups_ClusterizationWorkspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalTable: "ClusterizationWorkspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmbeddingObjectsGroups_DimensionalityReductionTechniques_DRTechniqueId",
                        column: x => x.DRTechniqueId,
                        principalTable: "DimensionalityReductionTechniques",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmbeddingObjectsGroups_EmbeddingModels_EmbeddingModelId",
                        column: x => x.EmbeddingModelId,
                        principalTable: "EmbeddingModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmbeddingObjectsGroups_MyDataObject_DataObjectId",
                        column: x => x.DataObjectId,
                        principalTable: "MyDataObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExternalObjects",
                columns: table => new
                {
                    FullId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Session = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataObjectId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalObjects", x => x.FullId);
                    table.ForeignKey(
                        name: "FK_ExternalObjects_MyDataObject_DataObjectId",
                        column: x => x.DataObjectId,
                        principalTable: "MyDataObject",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TelegramMessages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TelegramID = table.Column<int>(type: "int", nullable: false),
                    EditDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostAuthor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Views = table.Column<int>(type: "int", nullable: false),
                    LoadedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TelegramChannelId = table.Column<long>(type: "bigint", nullable: false),
                    TelegramRepliesCount = table.Column<int>(type: "int", nullable: false),
                    LoaderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DataObjectId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_AspNetUsers_LoaderId",
                        column: x => x.LoaderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_MyDataObject_DataObjectId",
                        column: x => x.DataObjectId,
                        principalTable: "MyDataObject",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramChannels_TelegramChannelId",
                        column: x => x.TelegramChannelId,
                        principalTable: "TelegramChannels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "YoutubeComments",
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
                    DataObjectId = table.Column<long>(type: "bigint", nullable: true),
                    LoaderId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YoutubeComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YoutubeComments_AspNetUsers_LoaderId",
                        column: x => x.LoaderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_YoutubeComments_MyDataObject_DataObjectId",
                        column: x => x.DataObjectId,
                        principalTable: "MyDataObject",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_YoutubeComments_YoutubeChannels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "YoutubeChannels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_YoutubeComments_YoutubeVideos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "YoutubeVideos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MyDataObjectWorkspaceDataObjectsAddPack",
                columns: table => new
                {
                    DataObjectsId = table.Column<long>(type: "bigint", nullable: false),
                    WorkspaceDataObjectsAddPacksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyDataObjectWorkspaceDataObjectsAddPack", x => new { x.DataObjectsId, x.WorkspaceDataObjectsAddPacksId });
                    table.ForeignKey(
                        name: "FK_MyDataObjectWorkspaceDataObjectsAddPack_MyDataObject_DataObjectsId",
                        column: x => x.DataObjectsId,
                        principalTable: "MyDataObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MyDataObjectWorkspaceDataObjectsAddPack_WorkspaceDataObjectsAddPacks_WorkspaceDataObjectsAddPacksId",
                        column: x => x.WorkspaceDataObjectsAddPacksId,
                        principalTable: "WorkspaceDataObjectsAddPacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "EmbeddingLoadingStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsAllEmbeddingsLoaded = table.Column<bool>(type: "bit", nullable: false),
                    EmbeddingModelId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProfileId = table.Column<int>(type: "int", nullable: true),
                    AddPackId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmbeddingLoadingStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmbeddingLoadingStates_ClusterizationProfiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "ClusterizationProfiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmbeddingLoadingStates_EmbeddingModels_EmbeddingModelId",
                        column: x => x.EmbeddingModelId,
                        principalTable: "EmbeddingModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmbeddingLoadingStates_WorkspaceDataObjectsAddPacks_AddPackId",
                        column: x => x.AddPackId,
                        principalTable: "WorkspaceDataObjectsAddPacks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DimensionEmbeddingObjects",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    ValuesString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmbeddingObjectsGroupId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimensionEmbeddingObjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DimensionEmbeddingObjects_DimensionTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "DimensionTypes",
                        principalColumn: "DimensionCount",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DimensionEmbeddingObjects_EmbeddingObjectsGroups_EmbeddingObjectsGroupId",
                        column: x => x.EmbeddingObjectsGroupId,
                        principalTable: "EmbeddingObjectsGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TelegramReplies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TelegramID = table.Column<long>(type: "bigint", nullable: false),
                    EditDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GroupedId = table.Column<long>(type: "bigint", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelegramMessageId = table.Column<long>(type: "bigint", nullable: false),
                    TelegramChannelId = table.Column<long>(type: "bigint", nullable: false),
                    LoaderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoadedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataObjectId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramReplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TelegramReplies_AspNetUsers_LoaderId",
                        column: x => x.LoaderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelegramReplies_MyDataObject_DataObjectId",
                        column: x => x.DataObjectId,
                        principalTable: "MyDataObject",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TelegramReplies_TelegramChannels_TelegramChannelId",
                        column: x => x.TelegramChannelId,
                        principalTable: "TelegramChannels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TelegramReplies_TelegramMessages_TelegramMessageId",
                        column: x => x.TelegramMessageId,
                        principalTable: "TelegramMessages",
                        principalColumn: "Id");
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClusterizationTiles_ClusterizationTiles_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ClusterizationTiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClusterMyDataObject",
                columns: table => new
                {
                    ClustersId = table.Column<int>(type: "int", nullable: false),
                    DataObjectsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterMyDataObject", x => new { x.ClustersId, x.DataObjectsId });
                    table.ForeignKey(
                        name: "FK_ClusterMyDataObject_Clusters_ClustersId",
                        column: x => x.ClustersId,
                        principalTable: "Clusters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClusterMyDataObject_MyDataObject_DataObjectsId",
                        column: x => x.DataObjectsId,
                        principalTable: "MyDataObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramReactions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Emotion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentId = table.Column<long>(type: "bigint", nullable: true),
                    IsCustom = table.Column<bool>(type: "bit", nullable: false),
                    TelegramMessageId = table.Column<long>(type: "bigint", nullable: true),
                    TelegramReplyId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramReactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TelegramReactions_TelegramMessages_TelegramMessageId",
                        column: x => x.TelegramMessageId,
                        principalTable: "TelegramMessages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TelegramReactions_TelegramReplies_TelegramReplyId",
                        column: x => x.TelegramReplyId,
                        principalTable: "TelegramReplies",
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
                    DataObjectId = table.Column<long>(type: "bigint", nullable: false),
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DisplayedPoints_MyDataObject_DataObjectId",
                        column: x => x.DataObjectId,
                        principalTable: "MyDataObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ClusterizationAlgorithmTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { "DBSCAN", "Density-Based Spatial Clustering Of Applications With Noise", "DBSCAN" },
                    { "GaussianMixture", "A clustering method that models the data as a mixture of Gaussian partitions", "Gaussian Mixture" },
                    { "KMeans", "Arrangement of a set of objects into relatively homogeneous groups.", "k-means" },
                    { "OneCluster", "Combining all elements into one cluster", "One cluster" },
                    { "SpectralClustering", "Spectral clustering is based on the principles of graph theory and linear algebra", "Spectral Clustering" }
                });

            migrationBuilder.InsertData(
                table: "ClusterizationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "External", "From file" },
                    { "TelegramMessages", "Telegram Messages" },
                    { "TelegramReplies", "Telegram Replies" },
                    { "YoutubeComments", "Youtube Comments" }
                });

            migrationBuilder.InsertData(
                table: "DimensionTypes",
                column: "DimensionCount",
                values: new object[]
                {
                    2,
                    3,
                    100,
                    1536,
                    3072
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
                table: "MyDataObjectType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "ExternalData", "External Data" },
                    { "TelegramMessage", "Telegram Message" },
                    { "TelegramReply", "Telegram Reply" },
                    { "YoutubeComment", "Youtube Comment" }
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
                    { "Telegram", "Loading data from Telegram", "Telegram" },
                    { "Youtube", "Loading data from Youtube", "Youtube" }
                });

            migrationBuilder.InsertData(
                table: "EmbeddingModels",
                columns: new[] { "Id", "Description", "DimensionTypeId", "MaxInputCount", "Name", "QuotasPrice" },
                values: new object[,]
                {
                    { "text-embedding-3-large", "Text embedding 3 large", 3072, 4000, "text_embedding_3_large", 8 },
                    { "text-embedding-3-small", "Text embedding 3 small", 1536, 4000, "text_embedding_3_small", 1 },
                    { "text-embedding-ada-002", "Text embedding Ada 002", 1536, 4000, "text-embedding-ada-002", 5 }
                });

            migrationBuilder.InsertData(
                table: "QuotasPackItems",
                columns: new[] { "Id", "Count", "PackId", "TypeId" },
                values: new object[,]
                {
                    { 1, 1000, 1, "Youtube" },
                    { 2, 1000, 1, "Telegram" },
                    { 3, 2000, 1, "Embeddings" },
                    { 4, 10000, 1, "Clustering" },
                    { 5, 5, 1, "PublicWorkspaces" },
                    { 6, 20, 1, "PrivateWorkspaces" },
                    { 7, 20, 1, "PublicProfiles" },
                    { 8, 50, 1, "PrivateProfiles" },
                    { 9, 1000000000, 2, "Youtube" },
                    { 10, 1000000000, 2, "Telegram" },
                    { 11, 1000000000, 2, "Embeddings" },
                    { 12, 1000000000, 2, "Clustering" },
                    { 13, 1000000000, 2, "PublicWorkspaces" },
                    { 14, 1000000000, 2, "PrivateWorkspaces" },
                    { 15, 1000000000, 2, "PublicProfiles" },
                    { 16, 1000000000, 2, "PrivateProfiles" }
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
                name: "IX_ClusterizationAbstractAlgorithms_TypeId",
                table: "ClusterizationAbstractAlgorithms",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationProfiles_AlgorithmId",
                table: "ClusterizationProfiles",
                column: "AlgorithmId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationProfiles_DimensionCount",
                table: "ClusterizationProfiles",
                column: "DimensionCount");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationProfiles_DRTechniqueId",
                table: "ClusterizationProfiles",
                column: "DRTechniqueId");

            migrationBuilder.CreateIndex(
                name: "IX_ClusterizationProfiles_EmbeddingModelId",
                table: "ClusterizationProfiles",
                column: "EmbeddingModelId");

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
                name: "IX_ClusterizationWorkspaceMyDataObject_WorkspacesId",
                table: "ClusterizationWorkspaceMyDataObject",
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
                name: "IX_ClusterMyDataObject_DataObjectsId",
                table: "ClusterMyDataObject",
                column: "DataObjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_Clusters_ParentClusterId",
                table: "Clusters",
                column: "ParentClusterId");

            migrationBuilder.CreateIndex(
                name: "IX_Clusters_ProfileId",
                table: "Clusters",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerQuotas_CustomerId",
                table: "CustomerQuotas",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerQuotas_TypeId",
                table: "CustomerQuotas",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DimensionEmbeddingObjects_EmbeddingObjectsGroupId",
                table: "DimensionEmbeddingObjects",
                column: "EmbeddingObjectsGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_DimensionEmbeddingObjects_TypeId",
                table: "DimensionEmbeddingObjects",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DisplayedPoints_ClusterId",
                table: "DisplayedPoints",
                column: "ClusterId");

            migrationBuilder.CreateIndex(
                name: "IX_DisplayedPoints_DataObjectId",
                table: "DisplayedPoints",
                column: "DataObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_DisplayedPoints_TileId",
                table: "DisplayedPoints",
                column: "TileId");

            migrationBuilder.CreateIndex(
                name: "IX_EmbeddingLoadingStates_AddPackId",
                table: "EmbeddingLoadingStates",
                column: "AddPackId");

            migrationBuilder.CreateIndex(
                name: "IX_EmbeddingLoadingStates_EmbeddingModelId",
                table: "EmbeddingLoadingStates",
                column: "EmbeddingModelId");

            migrationBuilder.CreateIndex(
                name: "IX_EmbeddingLoadingStates_ProfileId",
                table: "EmbeddingLoadingStates",
                column: "ProfileId",
                unique: true,
                filter: "[ProfileId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EmbeddingModels_DimensionTypeId",
                table: "EmbeddingModels",
                column: "DimensionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmbeddingObjectsGroups_DataObjectId",
                table: "EmbeddingObjectsGroups",
                column: "DataObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EmbeddingObjectsGroups_DRTechniqueId",
                table: "EmbeddingObjectsGroups",
                column: "DRTechniqueId");

            migrationBuilder.CreateIndex(
                name: "IX_EmbeddingObjectsGroups_EmbeddingModelId",
                table: "EmbeddingObjectsGroups",
                column: "EmbeddingModelId");

            migrationBuilder.CreateIndex(
                name: "IX_EmbeddingObjectsGroups_WorkspaceId",
                table: "EmbeddingObjectsGroups",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalObjects_DataObjectId",
                table: "ExternalObjects",
                column: "DataObjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyDataObject_TypeId",
                table: "MyDataObject",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MyDataObjectWorkspaceDataObjectsAddPack_WorkspaceDataObjectsAddPacksId",
                table: "MyDataObjectWorkspaceDataObjectsAddPack",
                column: "WorkspaceDataObjectsAddPacksId");

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
                name: "IX_TelegramChannels_LoaderId",
                table: "TelegramChannels",
                column: "LoaderId");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_DataObjectId",
                table: "TelegramMessages",
                column: "DataObjectId",
                unique: true,
                filter: "[DataObjectId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_LoaderId",
                table: "TelegramMessages",
                column: "LoaderId");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_TelegramChannelId",
                table: "TelegramMessages",
                column: "TelegramChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramReactions_TelegramMessageId",
                table: "TelegramReactions",
                column: "TelegramMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramReactions_TelegramReplyId",
                table: "TelegramReactions",
                column: "TelegramReplyId");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramReplies_DataObjectId",
                table: "TelegramReplies",
                column: "DataObjectId",
                unique: true,
                filter: "[DataObjectId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramReplies_LoaderId",
                table: "TelegramReplies",
                column: "LoaderId");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramReplies_TelegramChannelId",
                table: "TelegramReplies",
                column: "TelegramChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramReplies_TelegramMessageId",
                table: "TelegramReplies",
                column: "TelegramMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkspaceDataObjectsAddPacks_OwnerId",
                table: "WorkspaceDataObjectsAddPacks",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkspaceDataObjectsAddPacks_WorkspaceId",
                table: "WorkspaceDataObjectsAddPacks",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_YoutubeComments_DataObjectId",
                table: "YoutubeComments",
                column: "DataObjectId",
                unique: true,
                filter: "[DataObjectId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_YoutubeComments_ChannelId",
                table: "YoutubeComments",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_YoutubeComments_LoaderId",
                table: "YoutubeComments",
                column: "LoaderId");

            migrationBuilder.CreateIndex(
                name: "IX_YoutubeComments_PublishedAtDateTimeOffset",
                table: "YoutubeComments",
                column: "PublishedAtDateTimeOffset");

            migrationBuilder.CreateIndex(
                name: "IX_YoutubeComments_VideoId",
                table: "YoutubeComments",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_YoutubeChannels_LoaderId",
                table: "YoutubeChannels",
                column: "LoaderId");

            migrationBuilder.CreateIndex(
                name: "IX_YoutubeChannels_PublishedAtDateTimeOffset_VideoCount_SubscriberCount",
                table: "YoutubeChannels",
                columns: new[] { "PublishedAtDateTimeOffset", "VideoCount", "SubscriberCount" });

            migrationBuilder.CreateIndex(
                name: "IX_YoutubeVideos_ChannelId",
                table: "YoutubeVideos",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_YoutubeVideos_LoaderId",
                table: "YoutubeVideos",
                column: "LoaderId");

            migrationBuilder.CreateIndex(
                name: "IX_YoutubeVideos_PublishedAtDateTimeOffset_CommentCount_ViewCount",
                table: "YoutubeVideos",
                columns: new[] { "PublishedAtDateTimeOffset", "CommentCount", "ViewCount" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "ClusterizationWorkspaceMyDataObject");

            migrationBuilder.DropTable(
                name: "ClusterMyDataObject");

            migrationBuilder.DropTable(
                name: "CustomerQuotas");

            migrationBuilder.DropTable(
                name: "DimensionEmbeddingObjects");

            migrationBuilder.DropTable(
                name: "DisplayedPoints");

            migrationBuilder.DropTable(
                name: "EmbeddingLoadingStates");

            migrationBuilder.DropTable(
                name: "ExternalObjects");

            migrationBuilder.DropTable(
                name: "MyDataObjectWorkspaceDataObjectsAddPack");

            migrationBuilder.DropTable(
                name: "MyTasks");

            migrationBuilder.DropTable(
                name: "QuotasLogs");

            migrationBuilder.DropTable(
                name: "QuotasPackItems");

            migrationBuilder.DropTable(
                name: "QuotasPackLogs");

            migrationBuilder.DropTable(
                name: "TelegramReactions");

            migrationBuilder.DropTable(
                name: "YoutubeComments");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "EmbeddingObjectsGroups");

            migrationBuilder.DropTable(
                name: "ClusterizationTiles");

            migrationBuilder.DropTable(
                name: "Clusters");

            migrationBuilder.DropTable(
                name: "WorkspaceDataObjectsAddPacks");

            migrationBuilder.DropTable(
                name: "MyTaskStates");

            migrationBuilder.DropTable(
                name: "QuotasTypes");

            migrationBuilder.DropTable(
                name: "QuotasPacks");

            migrationBuilder.DropTable(
                name: "TelegramReplies");

            migrationBuilder.DropTable(
                name: "YoutubeVideos");

            migrationBuilder.DropTable(
                name: "ClusterizationTilesLevels");

            migrationBuilder.DropTable(
                name: "TelegramMessages");

            migrationBuilder.DropTable(
                name: "YoutubeChannels");

            migrationBuilder.DropTable(
                name: "ClusterizationProfiles");

            migrationBuilder.DropTable(
                name: "MyDataObject");

            migrationBuilder.DropTable(
                name: "TelegramChannels");

            migrationBuilder.DropTable(
                name: "ClusterizationAbstractAlgorithms");

            migrationBuilder.DropTable(
                name: "ClusterizationWorkspaces");

            migrationBuilder.DropTable(
                name: "DimensionalityReductionTechniques");

            migrationBuilder.DropTable(
                name: "EmbeddingModels");

            migrationBuilder.DropTable(
                name: "MyDataObjectType");

            migrationBuilder.DropTable(
                name: "ClusterizationAlgorithmTypes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ClusterizationTypes");

            migrationBuilder.DropTable(
                name: "DimensionTypes");
        }
    }
}

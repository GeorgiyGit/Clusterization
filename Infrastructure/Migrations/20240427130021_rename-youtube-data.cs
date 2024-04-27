using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class renameyoutubedata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "Channels");

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
                name: "YoutubeComments");

            migrationBuilder.DropTable(
                name: "YoutubeVideos");

            migrationBuilder.DropTable(
                name: "YoutubeChannels");

            migrationBuilder.CreateTable(
                name: "Channels",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoaderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChannelImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ETag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoadedCommentCount = table.Column<int>(type: "int", nullable: false),
                    LoadedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoadedVideoCount = table.Column<int>(type: "int", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishedAtDateTimeOffset = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PublishedAtRaw = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubscriberCount = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoCount = table.Column<int>(type: "int", nullable: false),
                    ViewCount = table.Column<long>(type: "bigint", nullable: false)
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
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChannelId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoaderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChannelTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentCount = table.Column<int>(type: "int", nullable: false),
                    DefaultAudioLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ETag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LikeCount = table.Column<int>(type: "int", nullable: false),
                    LiveBroadcaseContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoadedCommentCount = table.Column<int>(type: "int", nullable: false),
                    LoadedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishedAtDateTimeOffset = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PublishedAtRaw = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ViewCount = table.Column<long>(type: "bigint", nullable: false)
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DataObjectId = table.Column<long>(type: "bigint", nullable: true),
                    ChannelId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoaderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VideoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AuthorChannelId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorChannelUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorProfileImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CanReply = table.Column<bool>(type: "bit", nullable: false),
                    ETag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LikeCount = table.Column<int>(type: "int", nullable: false),
                    LoadedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishedAtDateTimeOffset = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PublishedAtRaw = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextDisplay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextOriginal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalReplyCount = table.Column<short>(type: "smallint", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAtDateTimeOffset = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAtRaw = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_MyDataObject_DataObjectId",
                        column: x => x.DataObjectId,
                        principalTable: "MyDataObject",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_DataObjectId",
                table: "Comments",
                column: "DataObjectId",
                unique: true,
                filter: "[DataObjectId] IS NOT NULL");

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
                name: "IX_Channels_LoaderId",
                table: "Channels",
                column: "LoaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Channels_PublishedAtDateTimeOffset_VideoCount_SubscriberCount",
                table: "Channels",
                columns: new[] { "PublishedAtDateTimeOffset", "VideoCount", "SubscriberCount" });

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
        }
    }
}

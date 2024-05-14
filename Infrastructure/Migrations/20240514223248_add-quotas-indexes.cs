using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addquotasindexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_YoutubeVideos_PublishedAtDateTimeOffset_CommentCount_ViewCount",
                table: "YoutubeVideos");

            migrationBuilder.CreateIndex(
                name: "IX_YoutubeVideos_PublishedAtDateTimeOffset_CommentCount_ViewCount_ChannelId",
                table: "YoutubeVideos",
                columns: new[] { "PublishedAtDateTimeOffset", "CommentCount", "ViewCount", "ChannelId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_YoutubeVideos_PublishedAtDateTimeOffset_CommentCount_ViewCount_ChannelId",
                table: "YoutubeVideos");

            migrationBuilder.CreateIndex(
                name: "IX_YoutubeVideos_PublishedAtDateTimeOffset_CommentCount_ViewCount",
                table: "YoutubeVideos",
                columns: new[] { "PublishedAtDateTimeOffset", "CommentCount", "ViewCount" });
        }
    }
}
